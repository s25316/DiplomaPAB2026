// Ignore Spelling: Teryt
using GUS.TERYT.Database;
using GUS.TERYT.Database.Models;
using GUS.TERYT.Database.Models.Simcs;
using GUS.TERYT.Database.Models.Tercs;
using GUS.TERYT.Database.Models.Ulics;
using GUS.TERYT.Files;
using GUS.TERYT.Files.Models.Adapted;
using GUS.TERYT.Infrastructure.Configurations;
using GUS.TERYT.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace GUS.TERYT.API.BackgroundServices;

public class TerytSeedBackgroundService(IServiceScopeFactory scopeFactory) : BackgroundService
{
    private readonly Dictionary<(string W, string P, string G, string GR), Guid> gminyIds = [];


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        using (var scope = scopeFactory.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<TerytDbContext>();
            var blobService = scope.ServiceProvider.GetRequiredService<IBlobService>();
            var configuration = scope.ServiceProvider.GetRequiredService<IOptions<TerytFilesConfiguration>>().Value;

            try
            {
                var sw = new Stopwatch();
                sw.Start();
                await context.Database.BeginTransactionAsync(stoppingToken);
                await SeedTercAsync(context, blobService, configuration, stoppingToken);
                await SeedSimcAsync(context, blobService, configuration, stoppingToken);
                await SeedUlicAsync(context, blobService, configuration, stoppingToken);
                await SeedSimcUlicAsync(context, blobService, configuration, stoppingToken);
                await context.Database.CommitTransactionAsync(stoppingToken);
                sw.Stop();
                Console.WriteLine($"Seed in time: {sw.ElapsedMilliseconds / 1000} sec");
            }
            catch
            {
                await context.Database.RollbackTransactionAsync(stoppingToken);
                throw;
            }
        }
    }

    private async Task SeedTercAsync(
        TerytDbContext context,
        IBlobService blobService,
        TerytFilesConfiguration configuration,
        CancellationToken cancellationToken)
    {
        var wojewodstwaCount = await context.Wojewodstwa.CountAsync(cancellationToken);
        var powiatyCount = await context.Powiaty.CountAsync(cancellationToken);
        var gminyCount = await context.Gminy.CountAsync(cancellationToken);
        var powiatTypesCount = await context.PowiatTypes.CountAsync(cancellationToken);
        var gminaRodzajeCount = await context.GminaRodzaje.CountAsync(cancellationToken);

        if (wojewodstwaCount != 0 &&
            powiatyCount != 0 &&
            gminyCount != 0 &&
            powiatTypesCount != 0 &&
            gminaRodzajeCount != 0)
        {
            return;
        }

        await context.Gminy.ExecuteDeleteAsync(cancellationToken);
        await context.Powiaty.ExecuteDeleteAsync(cancellationToken);
        await context.Wojewodstwa.ExecuteDeleteAsync(cancellationToken);
        await context.PowiatTypes.ExecuteDeleteAsync(cancellationToken);
        await context.GminaRodzaje.ExecuteDeleteAsync(cancellationToken);

        using var streamReader = await blobService.GetStreamReaderAsync(
            configuration.BlobContainer,
            configuration.Terc,
            cancellationToken);
        using var reader = new TerytAdaptedReader<Teryt.Terc>(streamReader);

        var powiatTypes = new Dictionary<string, PowiatType>();
        var gminaRodz = new Dictionary<string, GminaRodz>();


        foreach (var (key, value) in Teryt.Terc.Gmina.Type.All)
        {
            gminaRodz[key] = new GminaRodz
            {
                GminaRodzCode = value.Id,
                Name = value.Name,
            };
        }
        await context.GminaRodzaje.AddRangeAsync(gminaRodz.Values, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);


        await foreach (var item in reader.ReadAsync())
        {
            if (item is Teryt.Terc.Wojewodstwo w)
            {
                var dbItem = new Wojewodstwo
                {
                    WojewodstwoId = w.WojewodstwoId.WojewodstwoCode,
                    Name = w.Nazwa,
                };
                await context.Wojewodstwa.AddAsync(dbItem, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
            }

            if (item is Teryt.Terc.Powiat p)
            {
                if (!powiatTypes.TryGetValue(p.NazwaDod, out var powiatType))
                {
                    var maxId = !powiatTypes.Any()
                        ? 0
                        : powiatTypes.Values.Max(t => t.TypeId);

                    maxId++;
                    powiatType = new PowiatType
                    {
                        TypeId = maxId,
                        Name = p.NazwaDod,
                    };
                    powiatTypes[powiatType.Name] = powiatType;
                    await context.PowiatTypes.AddAsync(powiatType, cancellationToken);
                }

                var dbItem = new Powiat
                {
                    WojewodstwoId = p.PowiatId.WojewodstwoCode,
                    PowiatCode = p.PowiatId.PowiatCode,
                    Name = p.Nazwa,
                    PowiatType = powiatType
                };
                await context.Powiaty.AddAsync(dbItem, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
            }

            if (item is Teryt.Terc.Gmina g)
            {
                var dbPowiat = await context.Powiaty.FirstOrDefaultAsync(i =>
                    i.WojewodstwoId == g.GminaId.WojewodstwoCode &&
                    i.PowiatCode == g.GminaId.PowiatCode,
                    cancellationToken)
                    ?? throw new KeyNotFoundException("Not Found Powiat");

                var dbItem = new Gmina
                {
                    GminaCode = g.GminaId.GminaCode,
                    GminaRodzCode = g.GminaId.GminaRodzCode.Id,
                    Name = g.Nazwa,
                    Powiat = dbPowiat,
                };
                await context.Gminy.AddAsync(dbItem, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                gminyIds[(g.GminaId.WojewodstwoCode, g.GminaId.PowiatCode, g.GminaId.GminaCode, g.GminaId.GminaRodzCode.Id)] = dbItem.GminaId;
            }
        }
    }

    private async Task SeedSimcAsync(
        TerytDbContext context,
        IBlobService blobService,
        TerytFilesConfiguration configuration,
        CancellationToken cancellationToken)
    {
        var miejscowoscRodzajeCount = await context.MiejscowoscRodzaje.CountAsync(cancellationToken);
        var miejscowosciCount = await context.Miejscowosci.CountAsync(cancellationToken);

        if (miejscowoscRodzajeCount != 0 && miejscowosciCount != 0)
        {
            return;
        }

        await context.Miejscowosci.ExecuteDeleteAsync(cancellationToken);
        await context.MiejscowoscRodzaje.ExecuteDeleteAsync(cancellationToken);
        await context.SaveChangesAsync(cancellationToken);


        var rodzaje = new Dictionary<string, SimcRodzaj>();
        foreach (var (key, value) in Teryt.Simc.Type.All)
        {
            rodzaje[value.Id] = new SimcRodzaj
            {
                RodzajCode = value.Id,
                Name = value.Name,
            };
        }
        await context.MiejscowoscRodzaje.AddRangeAsync(rodzaje.Values, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);


        using var streamReader = await blobService.GetStreamReaderAsync(
            configuration.BlobContainer,
            configuration.Simc,
            cancellationToken);
        using var reader = new TerytAdaptedReader<Teryt.Simc>(streamReader);

        var list = new List<Simc>();
        await foreach (var item in reader.ReadAsync())
        {
            var gminaId = gminyIds[(item.GminaId.WojewodstwoCode, item.GminaId.PowiatCode, item.GminaId.GminaCode, item.GminaId.GminaRodzCode.Id)];
            var dbItem = new Simc
            {
                MiejscowoscId = item.MiejscowoscId.Value,
                Name = item.Nazwa,
                Rodzaj = rodzaje[item.MiejscowoscRodzaj.Id],
                GminaId = gminaId,
            };
            list.Add(dbItem);

            if (list.Count > 1000)
            {
                await context.Miejscowosci.AddRangeAsync(list, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                list.Clear();
            }
        }

        await context.Miejscowosci.AddRangeAsync(list, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        list.Clear();
    }

    private static async Task SeedUlicAsync(
        TerytDbContext context,
        IBlobService blobService,
        TerytFilesConfiguration configuration,
        CancellationToken cancellationToken)
    {
        var ulicTypesCount = await context.UlicTypes.CountAsync(cancellationToken);
        var ulicCount = await context.Ulicy.CountAsync(cancellationToken);

        if (ulicCount != 0 && ulicTypesCount != 0)
        {
            return;
        }
;
        await context.Ulicy.ExecuteDeleteAsync(cancellationToken);
        await context.UlicTypes.ExecuteDeleteAsync(cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        using var streamReader = await blobService.GetStreamReaderAsync(
            configuration.BlobContainer,
            configuration.Ulic,
            cancellationToken);
        using var reader = new TerytAdaptedReader<Teryt.Ulic>(streamReader);

        var ids = new HashSet<string>();
        var ulicTypes = new Dictionary<string, UlicType>();
        var list = new List<Ulic>();

        await foreach (var item in reader.ReadAsync())
        {
            if (ids.Contains(item.UlicId))
            {
                continue;
            }

            UlicType? dbUlicType = null;
            var baseUlicType = item.UlicType;

            if (baseUlicType is not null &&
                !ulicTypes.TryGetValue(baseUlicType.Value, out dbUlicType))
            {
                var maxId = !ulicTypes.Any()
                    ? 0
                    : ulicTypes.Values.Max(i => i.TypeId);

                maxId++;
                dbUlicType = new UlicType
                {
                    TypeId = maxId,
                    Name = baseUlicType.Value,
                };
                ulicTypes[dbUlicType.Name] = dbUlicType;
                await context.UlicTypes.AddAsync(dbUlicType, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
            }

            var dbUlic = new Ulic
            {
                UlicId = item.UlicId.Value,
                Name = $"{item.Nazwa1} {item.Nazwa2}".Trim(),
                Type = dbUlicType,
            };
            ids.Add(dbUlic.UlicId);
            list.Add(dbUlic);

            if (list.Count > 1000)
            {
                await context.Ulicy.AddRangeAsync(list, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                list.Clear();
            }
        }

        await context.Ulicy.AddRangeAsync(list, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        list.Clear();
    }

    private static async Task SeedSimcUlicAsync(
        TerytDbContext context,
        IBlobService blobService,
        TerytFilesConfiguration configuration,
        CancellationToken cancellationToken)
    {
        var connectionCount = await context.SimcUlics.CountAsync(cancellationToken);

        if (connectionCount != 0)
        {
            return;
        }



        var count1 = await context.Miejscowosci.CountAsync(cancellationToken);
        var count2 = await context.Ulicy.CountAsync(cancellationToken);


        using var streamReader = await blobService.GetStreamReaderAsync(
            configuration.BlobContainer,
            configuration.Ulic,
            cancellationToken);
        using var reader = new TerytAdaptedReader<Teryt.SimcUlicIds>(streamReader);

        var list = new List<SimcUlic>();

        await foreach (var item in reader.ReadAsync())
        {
            var dbItem = new SimcUlic
            {
                MiejscowoscId = item.SimcId.Value,
                UlicId = item.UlicId.Value,
            };
            list.Add(dbItem);

            if (list.Count > 1000)
            {
                await context.SimcUlics.AddRangeAsync(list, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                list.Clear();
            }
        }

        await context.SimcUlics.AddRangeAsync(list, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        list.Clear();
    }
}
