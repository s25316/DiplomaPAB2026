using Base.Models.ValueObjects.Regony;
using GUS.REGON.Application.Interfaces;
using GUS.REGON.Database;
using GUS.REGON.Database.Models.Addresses;
using GUS.REGON.Database.Models.Pkds;
using GUS.REGON.Database.Models.RegistrationDetails;
using GUS.REGON.Infrastructure.Configurations;
using GUS.REGON.Infrastructure.QueryBuilders;
using GUS.REGON.Models;
using GUS.REGON.Models.Responses;
using GUS.REGON.Models.Responses.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;
using static GUS.REGON.Models.RaportJednostki;
using DatabaseAddress = GUS.REGON.Database.Models.Addresses.Address;
using DatabaseInstitution = GUS.REGON.Database.Models.Institution;
using DatabaseRequest = GUS.REGON.Database.Models.Request;
using DatabaseRequestStatusCode = GUS.REGON.Database.Enums.RequestStatusCode;
using ResponseStatus = GUS.REGON.Models.Responses.Report.Status;

namespace GUS.REGON.Infrastructure.Repositories;

public class RequestRepository(
    IRegonService regonService,
    RegonDbContext context,
    RequestQueryBuilder baseQueryBuilder,
    IOptions<UpdateDataConfiguration> optionsUpdate) : IRequestRepository
{
    private sealed record CombineReport
    {
        [MemberNotNullWhen(true, nameof(DaneSzukaj), nameof(Jednostki), nameof(Pkds))]
        [MemberNotNullWhen(false, nameof(KomunikatKod))]
        public required bool IsSuccess { get; init; } = false;
        public required Regon Regon { get; init; } = null!;
        public required KomunikatKod? KomunikatKod { get; init; } = null;
        public DaneSzukaj? DaneSzukaj { get; init; } = null;
        public RaportJednostki? Jednostki { get; init; } = null;
        public IEnumerable<RaportPkd>? Pkds { get; init; } = [];
    }


    public async Task<IEnumerable<Report.Full>> GetAsync(
        QueryParameters parameters,
        CancellationToken cancellationToken = default)
    {
        var regons = parameters.Regons.ToHashSet();

        var updateIntervalDays = optionsUpdate.Value.UpdateIntervalDays;
        var today = DateOnly.FromDateTime(DateTimeOffset.Now.Date);
        var updateIntervalDay = today.AddDays(-updateIntervalDays);

        var databaseDictionary = await baseQueryBuilder
            .WithRegons(regons)
            .WithAsNoTracking()
            .Build()
            .ToDictionaryAsync(k => Regon.Parse(k.Regon), cancellationToken);
        var updatedDictionary = databaseDictionary
            .Where(i => i.Value.LastUpdate >= updateIntervalDay)
            .ToDictionary();

        var updatedKeys = updatedDictionary.Keys.ToHashSet();

        if (updatedKeys.Count == regons.Count)
            return Map(updatedDictionary.Values);


        var allKeys = databaseDictionary.Keys.ToHashSet();

        var updatingKeys = allKeys.Except(updatedKeys);
        var notExistingKeys = regons.Except(allKeys);

        var createOrUpdateKeys = updatingKeys
            .Concat(notExistingKeys)
            .ToHashSet();

        var regonResultsDictionary = await GetFromRegonAsync(createOrUpdateKeys, cancellationToken);

        if (!regonResultsDictionary.Any())
            return Map(updatedDictionary.Values);

        await EnsureDictionarresAsync(regonResultsDictionary.Values, cancellationToken);

        var databaseUpdatingItems = await baseQueryBuilder
            .WithRegons(updatingKeys)
            .Build()
            .ToDictionaryAsync(k => Regon.Parse(k.Regon), cancellationToken);

        foreach (var (regon, databseItem) in databaseUpdatingItems)
        {
            var regonReport = regonResultsDictionary[regon];
            await CreateOrUpdateRequestAsync(regonReport, databseItem, today, cancellationToken);
        }

        foreach (var regon in notExistingKeys)
        {
            var regonReport = regonResultsDictionary[regon];
            var databaseRequest = await CreateOrUpdateRequestAsync(regonReport, null, today, cancellationToken);

            if (databaseRequest.Institution is not null)
                await context.Institutions.AddAsync(databaseRequest.Institution);
            await context.Requests.AddAsync(databaseRequest, cancellationToken);
        }

        await context.SaveChangesAsync(cancellationToken);


        var databaseList = await baseQueryBuilder
            .WithRegons(regons)
            .WithAsNoTracking()
            .Build()
            .ToListAsync(cancellationToken);

        if (databaseList.Count != regons.Count)
            throw new InvalidOperationException($"Must be same Count of keys: {string.Join(',', regons.Select(r => r.To14SCharacters()))}");

        return Map(databaseList);
    }


    private async Task<Dictionary<Regon, CombineReport>> GetFromRegonAsync(HashSet<Regon> regons, CancellationToken cancellationToken = default)
    {
        var resultDictionary = new Dictionary<Regon, CombineReport>();

        foreach (var regon in regons)
        {
            var daneSzukajResult = await regonService.GetDaneSzukajAsync(regon, cancellationToken);

            if (daneSzukajResult.StatusUslugi is not StatusUslugi.UslugaDostepna)
                return [];

            if (daneSzukajResult.IsFailure)
            {
                resultDictionary[regon] = new CombineReport
                {
                    IsSuccess = false,
                    Regon = regon,
                    KomunikatKod = daneSzukajResult.KomunikatKod,
                };
                continue;
            }


            var raportJednostkiResult = await regonService.GetRaportJednostkiAsync(
                regon,
                daneSzukajResult.Value.First().Typ,
                daneSzukajResult.Value.First().SilosId,
                cancellationToken);

            if (raportJednostkiResult.StatusUslugi is not StatusUslugi.UslugaDostepna)
                return [];

            if (raportJednostkiResult.IsFailure)
            {
                resultDictionary[regon] = new CombineReport
                {
                    IsSuccess = false,
                    Regon = regon,
                    KomunikatKod = raportJednostkiResult.KomunikatKod,
                };
                continue;
            }


            var pkdResult = await regonService.GetPkdJednostkiAsync(
                regon,
                daneSzukajResult.Value.First().Typ,
                cancellationToken);

            if (raportJednostkiResult.StatusUslugi is not StatusUslugi.UslugaDostepna)
                return [];

            if (pkdResult.IsFailure)
            {
                resultDictionary[regon] = new CombineReport
                {
                    IsSuccess = false,
                    Regon = regon,
                    KomunikatKod = pkdResult.KomunikatKod,
                };
                continue;
            }

            resultDictionary[regon] = new CombineReport
            {
                IsSuccess = true,
                Regon = regon,
                KomunikatKod = null,
                DaneSzukaj = daneSzukajResult.Value.First(),
                Jednostki = raportJednostkiResult.Value,
                Pkds = pkdResult.Value,
            };
        }
        return resultDictionary;
    }

    private async Task EnsureDictionaryItemsExistAsync<TEntity>(
        IEnumerable<CombineReport> reports,
        Func<CombineReport, IEnumerable<RaportJednostki.Pair>> getPairs,
        Func<RegonDbContext, IEnumerable<string>, CancellationToken, Task<Dictionary<string, TEntity>>> getDbDictionary,
        Func<RegonDbContext, DbSet<TEntity>> getDbSet,
        Func<RaportJednostki.Pair, TEntity> mapToDatabaseEntity,
        CancellationToken cancellationToken)
        where TEntity : class
    {
        var inputItems = reports
            .Where(i => i.IsSuccess)
            .SelectMany(getPairs);

        var inputDictionary = new Dictionary<string, Pair>();

        foreach (var item in inputItems)
        {
            inputDictionary[item.Symbol] = item;
        }

        if (!inputDictionary.Any())
            return;

        var keys = inputDictionary.Keys;
        var databaseDictionary = await getDbDictionary(context, keys, cancellationToken);

        var inputKeys = inputDictionary.Keys.ToHashSet();
        var databaseKeys = databaseDictionary.Keys.ToHashSet();

        var newKeys = inputKeys.Except(databaseKeys);

        if (!newKeys.Any())
            return;

        foreach (var key in newKeys)
        {
            var input = inputDictionary[key];

            if (input is null)
                continue;

            await getDbSet(context).AddAsync(mapToDatabaseEntity(input), cancellationToken);
        }
    }

    private Task EnsureDictionaryItemsExistAsync<TEntity>(
        IEnumerable<CombineReport> reports,
        Func<CombineReport, RaportJednostki.Pair?> getNullablePair,
        Func<RegonDbContext, IEnumerable<string>, CancellationToken, Task<Dictionary<string, TEntity>>> getDbDictionary,
        Func<RegonDbContext, DbSet<TEntity>> getDbSet,
        Func<RaportJednostki.Pair, TEntity> mapToDatabaseEntity,
        CancellationToken cancellationToken)
        where TEntity : class
    {
        Func<CombineReport, IEnumerable<RaportJednostki.Pair>> convertedPairsFunc = report =>
        {
            var pair = getNullablePair(report);
            return pair != null ? [pair] : [];
        };

        return EnsureDictionaryItemsExistAsync(
            reports,
            convertedPairsFunc,
            getDbDictionary,
            getDbSet,
            mapToDatabaseEntity,
            cancellationToken);
    }

    private async Task EnsureFormyFinansowaniaAsync(
        IEnumerable<CombineReport> reports,
        CancellationToken cancellationToken = default
    ) => await EnsureDictionaryItemsExistAsync<FormaFinansowania>(
        reports,
        report => report.Jednostki?.FormaFinansowania,
        (context, keys, cancellationToken) => context.FormyFinansowania.AsNoTracking().ToDictionaryAsync(k => k.FormaFinansowaniaCode, cancellationToken),
        context => context.FormyFinansowania,
        pair => new FormaFinansowania { FormaFinansowaniaCode = pair.Symbol, Name = pair.Nazwa },
        cancellationToken);

    private async Task EnsureFormyWlasnosciAsync(
        IEnumerable<CombineReport> reports,
        CancellationToken cancellationToken = default
    ) => await EnsureDictionaryItemsExistAsync<FormaWlasnosci>(
        reports,
        report => report.Jednostki?.FormaWlasnosci,
        (context, keys, cancellationToken) => context.FormyWlasnosci.AsNoTracking().ToDictionaryAsync(k => k.FormaWlasnosciCode, cancellationToken),
        context => context.FormyWlasnosci,
        pair => new FormaWlasnosci { FormaWlasnosciCode = pair.Symbol, Name = pair.Nazwa },
        cancellationToken);

    private async Task EnsureOrganyRejestroweAsync(
        IEnumerable<CombineReport> reports,
        CancellationToken cancellationToken = default
    ) => await EnsureDictionaryItemsExistAsync<OrganRejestrowy>(
        reports,
        report => report.Jednostki?.OrganRejestrowy,
        (context, keys, cancellationToken) => context.OrganyRejestrowe.AsNoTracking().ToDictionaryAsync(k => k.OrganRejestrowyCode, cancellationToken),
        context => context.OrganyRejestrowe,
        pair => new OrganRejestrowy { OrganRejestrowyCode = pair.Symbol, Name = pair.Nazwa },
        cancellationToken);

    private async Task EnsureOrganyZalozycielskieAsync(
        IEnumerable<CombineReport> reports,
        CancellationToken cancellationToken = default
    ) => await EnsureDictionaryItemsExistAsync<OrganZalozycielski>(
        reports,
        report => report.Jednostki?.OrganZalozycielski,
        (context, keys, cancellationToken) => context.OrganyZalozycielskie.AsNoTracking().ToDictionaryAsync(k => k.OrganZalozycielskiCode, cancellationToken),
        context => context.OrganyZalozycielskie,
        pair => new OrganZalozycielski { OrganZalozycielskiCode = pair.Symbol, Name = pair.Nazwa },
        cancellationToken);

    private async Task EnsurePodstawoweFormyPrawneAsync(
        IEnumerable<CombineReport> reports,
        CancellationToken cancellationToken = default
    ) => await EnsureDictionaryItemsExistAsync<PodstawowaFormaPrawna>(
        reports,
        report => report.Jednostki?.PodstawowaFormaPrawna,
        (context, keys, cancellationToken) => context.PodstawoweFormyPrawne.AsNoTracking().ToDictionaryAsync(k => k.PodstawowaFormaPrawnaCode, cancellationToken),
        context => context.PodstawoweFormyPrawne,
        pair => new PodstawowaFormaPrawna { PodstawowaFormaPrawnaCode = pair.Symbol, Name = pair.Nazwa },
        cancellationToken);

    private async Task EnsureSzczegolneFormyPrawneAsync(
        IEnumerable<CombineReport> reports,
        CancellationToken cancellationToken = default
    ) => await EnsureDictionaryItemsExistAsync<SzczegolnaFormaPrawna>(
        reports,
        report => report.Jednostki?.SzczegolnaFormaPrawna,
        (context, keys, cancellationToken) => context.SzczegolneFormyPrawne.AsNoTracking().ToDictionaryAsync(k => k.SzczegolnaFormaPrawnaCode, cancellationToken),
        context => context.SzczegolneFormyPrawne,
        pair => new SzczegolnaFormaPrawna { SzczegolnaFormaPrawnaCode = pair.Symbol, Name = pair.Nazwa },
        cancellationToken);

    private async Task EnsureRodzajeRejestruAsync(
        IEnumerable<CombineReport> reports,
        CancellationToken cancellationToken = default
    ) => await EnsureDictionaryItemsExistAsync<RodzajRejestru>(
        reports,
        report => report.Jednostki?.RodzajRejestru,
        (context, keys, cancellationToken) => context.RodzajeRejestru.AsNoTracking().ToDictionaryAsync(k => k.RodzajRejestruCode, cancellationToken),
        context => context.RodzajeRejestru,
        pair => new RodzajRejestru { RodzajRejestruCode = pair.Symbol, Name = pair.Nazwa },
        cancellationToken);

    private async Task EnsureKrajeAsync(
        IEnumerable<CombineReport> reports,
        CancellationToken cancellationToken = default
    ) => await EnsureDictionaryItemsExistAsync<Kraj>(
        reports,
        report => report.Jednostki?.Adres?.Kraj,
        (context, keys, cancellationToken) => context.Kraje.AsNoTracking().ToDictionaryAsync(k => k.KrajCode, cancellationToken),
        context => context.Kraje,
        pair => new Kraj { KrajCode = pair.Symbol, Name = pair.Nazwa },
        cancellationToken);

    private async Task EnsureWojewodztwaAsync(
        IEnumerable<CombineReport> reports,
        CancellationToken cancellationToken = default
    ) => await EnsureDictionaryItemsExistAsync<Wojewodztwo>(
        reports,
        report => report.Jednostki?.Adres?.Wojewodztwo,
        (context, keys, cancellationToken) => context.Wojewodztwa.AsNoTracking().ToDictionaryAsync(k => k.WojewodztwoCode, cancellationToken),
        context => context.Wojewodztwa,
        pair => new Wojewodztwo { WojewodztwoCode = pair.Symbol, Name = pair.Nazwa },
        cancellationToken);

    private async Task EnsurePowiatyAsync(
        IEnumerable<CombineReport> reports,
        CancellationToken cancellationToken = default
    ) => await EnsureDictionaryItemsExistAsync<Powiat>(
        reports,
        report => report.Jednostki?.Adres?.Powiat,
        (context, keys, cancellationToken) => context.Powiaty.AsNoTracking().ToDictionaryAsync(k => k.PowiatCode, cancellationToken),
        context => context.Powiaty,
        pair => new Powiat { PowiatCode = pair.Symbol, Name = pair.Nazwa },
        cancellationToken);

    private async Task EnsureGminyAsync(
        IEnumerable<CombineReport> reports,
        CancellationToken cancellationToken = default
    ) => await EnsureDictionaryItemsExistAsync<Gmina>(
        reports,
        report => report.Jednostki?.Adres?.Gmina,
        (context, keys, cancellationToken) => context.Gminy.AsNoTracking().ToDictionaryAsync(k => k.GminaCode, cancellationToken),
        context => context.Gminy,
        pair => new Gmina { GminaCode = pair.Symbol, Name = pair.Nazwa },
        cancellationToken);

    private async Task EnsureMiejscowosciPocztyAsync(
        IEnumerable<CombineReport> reports,
        CancellationToken cancellationToken = default
    ) => await EnsureDictionaryItemsExistAsync<MiejscowoscPoczty>(
        reports,
        report => report.Jednostki?.Adres?.MiejscowoscPoczty,
        (context, keys, cancellationToken) => context.MiejscowosciPoczty.AsNoTracking().ToDictionaryAsync(k => k.MiejscowoscPocztyCode, cancellationToken),
        context => context.MiejscowosciPoczty,
        pair => new MiejscowoscPoczty { MiejscowoscPocztyCode = pair.Symbol, Name = pair.Nazwa },
        cancellationToken);

    private async Task EnsureMiejscowosciAsync(
        IEnumerable<CombineReport> reports,
        CancellationToken cancellationToken = default
    ) => await EnsureDictionaryItemsExistAsync<Miejscowosc>(
        reports,
        report => report.Jednostki?.Adres?.Miejscowosc,
        (context, keys, cancellationToken) => context.Miejscowosci.AsNoTracking().ToDictionaryAsync(k => k.MiejscowoscCode, cancellationToken),
        context => context.Miejscowosci,
        pair => new Miejscowosc { MiejscowoscCode = pair.Symbol, Name = pair.Nazwa },
        cancellationToken);

    private async Task EnsureUlicyAsync(
        IEnumerable<CombineReport> reports,
        CancellationToken cancellationToken = default
    ) => await EnsureDictionaryItemsExistAsync<Ulica>(
        reports,
        report => report.Jednostki?.Adres?.Ulica,
        (context, keys, cancellationToken) => context.Ulicy.AsNoTracking().ToDictionaryAsync(k => k.UlicaCode, cancellationToken),
        context => context.Ulicy,
        pair => new Ulica { UlicaCode = pair.Symbol, Name = pair.Nazwa },
        cancellationToken);

    private async Task EnsurePkdsAsync(
        IEnumerable<CombineReport> reports,
        CancellationToken cancellationToken = default
    ) => await EnsureDictionaryItemsExistAsync<Pkd>(
        reports,
        report => report.Pkds?.Select(i => new RaportJednostki.Pair(i.Kod, i.Nazwa)) ?? [],
        (context, keys, cancellationToken) => context.Pkds.AsNoTracking().ToDictionaryAsync(k => k.PkdCode, cancellationToken),
        context => context.Pkds,
        pair => new Pkd { PkdCode = pair.Symbol, Name = pair.Nazwa },
        cancellationToken);

    private async Task EnsureDictionarresAsync(IEnumerable<CombineReport> items, CancellationToken cancellationToken)
    {
        await EnsureFormyFinansowaniaAsync(items, cancellationToken);
        await EnsureFormyWlasnosciAsync(items, cancellationToken);
        await EnsureOrganyRejestroweAsync(items, cancellationToken);
        await EnsureOrganyZalozycielskieAsync(items, cancellationToken);
        await EnsurePodstawoweFormyPrawneAsync(items, cancellationToken);
        await EnsureSzczegolneFormyPrawneAsync(items, cancellationToken);
        await EnsureRodzajeRejestruAsync(items, cancellationToken);

        await EnsureKrajeAsync(items, cancellationToken);
        await EnsureWojewodztwaAsync(items, cancellationToken);
        await EnsurePowiatyAsync(items, cancellationToken);
        await EnsureGminyAsync(items, cancellationToken);
        await EnsureMiejscowosciPocztyAsync(items, cancellationToken);
        await EnsureMiejscowosciAsync(items, cancellationToken);
        await EnsureUlicyAsync(items, cancellationToken);

        await EnsurePkdsAsync(items, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);
    }


    private async Task<DatabaseRequest> CreateOrUpdateRequestAsync(
        CombineReport report,
        DatabaseRequest? database,
        DateOnly today,
        CancellationToken cancellationToken = default)
    {
        database ??= new DatabaseRequest
        {
            Regon = report.Regon.To14SCharacters(),
        };
        database.LastUpdate = today;
        database.RequestStatusCode = (int)Map(report.KomunikatKod);

        if (!report.IsSuccess)
        {
            if (database.Institution is not null)
            {
                // Impossible Event
                context.Institutions.Remove(database.Institution);
            }
            return database;
        }

        database.Institution ??= new DatabaseInstitution { Request = database };

        database.Institution.SilosId = report.DaneSzukaj.SilosId;
        database.Institution.TypJednostkiCode = report.DaneSzukaj.Typ.ToString();

        database.Institution.Nazwa = report.Jednostki.Nazwa;
        database.Institution.NazwaSkrocona = report.Jednostki.NazwaSkrocona;
        database.Institution.NumerwRejestrzeEwidencji = report.Jednostki.NumerwRejestrzeEwidencji;
        database.Institution.Dzialalnosci = report.Jednostki.Dzialalnosci;

        database.Institution.FormaFinansowaniaCode = report.Jednostki.FormaFinansowania?.Symbol;
        database.Institution.OrganRejestrowyCode = report.Jednostki.OrganRejestrowy?.Symbol;
        database.Institution.RodzajRejestruCode = report.Jednostki.RodzajRejestru?.Symbol;
        database.Institution.PodstawowaFormaPrawnaCode = report.Jednostki.PodstawowaFormaPrawna?.Symbol;
        database.Institution.SzczegolnaFormaPrawnaCode = report.Jednostki.SzczegolnaFormaPrawna?.Symbol;
        database.Institution.OrganZalozycielskiCode = report.Jednostki.OrganZalozycielski?.Symbol;
        database.Institution.FormaWlasnosciCode = report.Jednostki.FormaWlasnosci?.Symbol;

        database.Institution.DataPowstania = report.Jednostki.Daty.DataPowstania;
        database.Institution.DataRozpoczecia = report.Jednostki.Daty.DataRozpoczecia;
        database.Institution.DataWpisu = report.Jednostki.Daty.DataWpisu;
        database.Institution.DataZawieszenia = report.Jednostki.Daty.DataZawieszenia;
        database.Institution.DataWznowienia = report.Jednostki.Daty.DataWznowienia;
        database.Institution.DataZmiany = report.Jednostki.Daty.DataZmiany;
        database.Institution.DataZakonczenia = report.Jednostki.Daty.DataZakonczenia;
        database.Institution.DataSkreslenia = report.Jednostki.Daty.DataSkreslenia;
        database.Institution.DataWpisuDoRejestruEwidencji = report.Jednostki.Daty.DataWpisuDoRejestruEwidencji;


        await CreateOrUpdatePkdAsync(report.Pkds, database.Institution, cancellationToken);

        if (report.Jednostki.Adres is null)
            database.Institution.Address = null;
        else
            database.Institution.Address = await CreateOrUpdateAddressAsync(report.Jednostki.Adres, database.Institution.Address, cancellationToken);

        return database;
    }

    private async Task<DatabaseAddress> CreateOrUpdateAddressAsync(RaportJednostki.Address item, DatabaseAddress? database, CancellationToken cancellationToken)
    {
        if (database is not null &&
            database.KrajCode == item.Kraj.Symbol &&
            database.WojewodztwoCode == item.Wojewodztwo.Symbol &&
            database.PowiatCode == item.Powiat.Symbol &&
            database.GminaCode == item.Gmina.Symbol &&
            database.MiejscowoscPocztyCode == item.MiejscowoscPoczty.Symbol &&
            database.MiejscowoscCode == item.Miejscowosc.Symbol &&
            database.UlicaCode == item.Ulica?.Symbol &&
            database.KodPocztowy == item.KodPocztowy &&
            database.NumerNieruchomosci == item.NumerNieruchomosci &&
            database.NumerLokalu == item.NumerLokalu &&
            database.NietypoweMiejsceLokalizacji == item.NietypoweMiejsceLokalizacji)
        {
            return database;
        }

        var ulicaCode = item.Ulica?.Symbol;
        database = await context
            .Addresses
            .FirstOrDefaultAsync(i =>
                i.KrajCode == item.Kraj.Symbol &&
                i.WojewodztwoCode == item.Wojewodztwo.Symbol &&
                i.PowiatCode == item.Powiat.Symbol &&
                i.GminaCode == item.Gmina.Symbol &&
                i.MiejscowoscPocztyCode == item.MiejscowoscPoczty.Symbol &&
                i.MiejscowoscCode == item.Miejscowosc.Symbol &&
                i.UlicaCode == ulicaCode &&
                i.KodPocztowy == item.KodPocztowy &&
                i.NumerNieruchomosci == item.NumerNieruchomosci &&
                i.NumerLokalu == item.NumerLokalu &&
                i.NietypoweMiejsceLokalizacji == item.NietypoweMiejsceLokalizacji
            , cancellationToken);

        if (database is not null)
            return database;

        database = new DatabaseAddress
        {
            KrajCode = item.Kraj.Symbol,
            WojewodztwoCode = item.Wojewodztwo.Symbol,
            PowiatCode = item.Powiat.Symbol,
            GminaCode = item.Gmina.Symbol,
            MiejscowoscPocztyCode = item.MiejscowoscPoczty.Symbol,
            MiejscowoscCode = item.Miejscowosc.Symbol,
            UlicaCode = item.Ulica?.Symbol,
            KodPocztowy = item.KodPocztowy,
            NumerNieruchomosci = item.NumerNieruchomosci,
            NumerLokalu = item.NumerLokalu,
            NietypoweMiejsceLokalizacji = item.NietypoweMiejsceLokalizacji,
        };
        await context.Addresses.AddAsync(database, cancellationToken);
        return database;
    }

    private async Task CreateOrUpdatePkdAsync(IEnumerable<RaportPkd> items, DatabaseInstitution database, CancellationToken cancellationToken)
    {
        var itemsDictionary = items.ToDictionary(k => k.Kod);
        var databaseDictionary = database.Pkds.ToDictionary(k => k.PkdCode);

        var itemsKeys = itemsDictionary.Keys;
        var databaseKeys = databaseDictionary.Keys;

        var addKeys = itemsKeys.Except(databaseKeys);
        var removeKeys = databaseKeys.Except(itemsKeys);

        foreach (var key in addKeys)
        {
            var item = itemsDictionary[key];
            var newDatabse = new InstitutionPkd
            {
                Institution = database,
                PkdCode = item.Kod,
                IsMain = item.IsMain,
            };
            await context.InstitutionPkds.AddAsync(newDatabse, cancellationToken);
            databaseDictionary[key] = newDatabse;
        }

        foreach (var key in removeKeys)
        {
            var databaseItem = databaseDictionary[key];
            context.InstitutionPkds.Remove(databaseItem);
        }

        foreach (var key in itemsKeys)
        {
            var item = itemsDictionary[key];
            var databaseItem = databaseDictionary[key];

            if (item.IsMain != databaseItem.IsMain)
                databaseItem.IsMain = item.IsMain;
        }
    }

    private static DatabaseRequestStatusCode Map(KomunikatKod? item) => item switch
    {
        null => DatabaseRequestStatusCode.Istneje,
        KomunikatKod.NieZnalezionoPodmiotów => DatabaseRequestStatusCode.NieIstneje,
        KomunikatKod.BrakUprawnienDoRaportu => DatabaseRequestStatusCode.BrakUprawnien,
        _ => throw new NotImplementedException(item.ToString()),
    };

    private static ResponseStatus Map(DatabaseRequestStatusCode item) => item switch
    {
        DatabaseRequestStatusCode.Istneje => ResponseStatus.Istneje,
        DatabaseRequestStatusCode.NieIstneje => ResponseStatus.NieIstneje,
        DatabaseRequestStatusCode.BrakUprawnien => ResponseStatus.BrakUprawnien,
        _ => throw new NotImplementedException(item.ToString()),
    };

    private static IEnumerable<Report.Full> Map(IEnumerable<DatabaseRequest> items) => items.Select(Map);

    private static Report.Full Map(DatabaseRequest item)
    {
        var organRejestrowy = item.Institution?.OrganRejestrowy is null
            ? null
            : new DictionaryItem
            {
                Code = item.Institution.OrganRejestrowy.OrganRejestrowyCode,
                Nazwa = item.Institution.OrganRejestrowy.Name
            };
        var rodzajRejestru = item.Institution?.RodzajRejestru is null
            ? null
            : new DictionaryItem
            {
                Code = item.Institution.RodzajRejestru.RodzajRejestruCode,
                Nazwa = item.Institution.RodzajRejestru.Name
            };
        var formaFinansowania = item.Institution?.FormaFinansowania is null
            ? null
            : new DictionaryItem
            {
                Code = item.Institution.FormaFinansowania.FormaFinansowaniaCode,
                Nazwa = item.Institution.FormaFinansowania.Name
            };
        var podstawowaFormaPrawna = item.Institution?.PodstawowaFormaPrawna is null
            ? null
            : new DictionaryItem
            {
                Code = item.Institution.PodstawowaFormaPrawna.PodstawowaFormaPrawnaCode,
                Nazwa = item.Institution.PodstawowaFormaPrawna.Name
            };
        var szczegolnaFormaPrawna = item.Institution?.SzczegolnaFormaPrawna is null
            ? null
            : new DictionaryItem
            {
                Code = item.Institution.SzczegolnaFormaPrawna.SzczegolnaFormaPrawnaCode,
                Nazwa = item.Institution.SzczegolnaFormaPrawna.Name
            };
        var organZalozycielski = item.Institution?.OrganZalozycielski is null
            ? null
            : new DictionaryItem
            {
                Code = item.Institution.OrganZalozycielski.OrganZalozycielskiCode,
                Nazwa = item.Institution.OrganZalozycielski.Name
            };
        var formaWlasnosci = item.Institution?.FormaWlasnosci is null
            ? null
            : new DictionaryItem
            {
                Code = item.Institution.FormaWlasnosci.FormaWlasnosciCode,
                Nazwa = item.Institution.FormaWlasnosci.Name
            };

        var ulica = item.Institution?.Address?.Ulica is null
            ? null
            : new DictionaryItem
            {
                Code = item.Institution.Address.Ulica.UlicaCode,
                Nazwa = item.Institution.Address.Ulica.Name
            };
        var address = item.Institution?.Address is null
            ? null
            : new Report.Institution.Address
            {
                KodPocztowy = item.Institution.Address.KodPocztowy,
                NumerNieruchomosci = item.Institution.Address.NumerNieruchomosci,
                NumerLokalu = item.Institution.Address.NumerLokalu,
                NietypoweMiejsceLokalizacji = item.Institution.Address.NietypoweMiejsceLokalizacji,
                Kraj = new DictionaryItem
                {
                    Code = item.Institution.Address.Kraj.KrajCode,
                    Nazwa = item.Institution.Address.Kraj.Name
                },
                Wojewodztwo = new DictionaryItem
                {
                    Code = item.Institution.Address.Wojewodztwo.WojewodztwoCode,
                    Nazwa = item.Institution.Address.Wojewodztwo.Name
                },
                Powiat = new DictionaryItem
                {
                    Code = item.Institution.Address.Powiat.PowiatCode,
                    Nazwa = item.Institution.Address.Powiat.Name
                },
                Gmina = new DictionaryItem
                {
                    Code = item.Institution.Address.Gmina.GminaCode,
                    Nazwa = item.Institution.Address.Gmina.Name
                },
                MiejscowoscPoczty = new DictionaryItem
                {
                    Code = item.Institution.Address.MiejscowoscPoczty.MiejscowoscPocztyCode,
                    Nazwa = item.Institution.Address.MiejscowoscPoczty.Name
                },
                Miejscowosc = new DictionaryItem
                {
                    Code = item.Institution.Address.Miejscowosc.MiejscowoscCode,
                    Nazwa = item.Institution.Address.Miejscowosc.Name
                },
                Ulica = ulica,
            };

        return new Report.Full
        {
            Regon = item.Regon,
            Status = Map((DatabaseRequestStatusCode)item.RequestStatusCode),
            Institution = item.Institution is null
            ? null
            : new Report.Institution
            {
                Regon = item.Institution.Regon,
                Nazwa = item.Institution.Nazwa,
                NazwaSkrocona = item.Institution.NazwaSkrocona,
                NumerwRejestrzeEwidencji = item.Institution.NumerwRejestrzeEwidencji,
                Dzialalnosci = item.Institution.Dzialalnosci,
                Daty = new Report.Institution.Dates
                {
                    DataPowstania = item.Institution.DataPowstania,
                    DataRozpoczecia = item.Institution.DataRozpoczecia,
                    DataWpisu = item.Institution.DataWpisu,
                    DataZawieszenia = item.Institution.DataZawieszenia,
                    DataWznowienia = item.Institution.DataWznowienia,
                    DataZmiany = item.Institution.DataZmiany,
                    DataZakonczenia = item.Institution.DataZakonczenia,
                    DataSkreslenia = item.Institution.DataSkreslenia,
                    DataWpisuDoRejestruEwidencji = item.Institution.DataWpisuDoRejestruEwidencji,
                },
                TypJednostki = new DictionaryItem
                {
                    Code = item.Institution.TypJednostki.TypJednostkiCode,
                    Nazwa = item.Institution.TypJednostki.Name
                },
                OrganRejestrowy = organRejestrowy,
                RodzajRejestru = rodzajRejestru,
                FormaFinansowania = formaFinansowania,
                PodstawowaFormaPrawna = podstawowaFormaPrawna,
                SzczegolnaFormaPrawna = szczegolnaFormaPrawna,
                OrganZalozycielski = organZalozycielski,
                FormaWlasnosci = formaWlasnosci,
                Adres = address,
            },
            Pkd = item.Institution is null
            ? null
            : new Report.Pkd
            {
                Items = item.Institution.Pkds.Select(i => new Report.Pkd.Item
                {
                    Pkd = new DictionaryItem
                    {
                        Code = i.Pkd.PkdCode,
                        Nazwa = i.Pkd.Name
                    },
                    IsMain = i.IsMain
                }),
            },
        };
    }
}