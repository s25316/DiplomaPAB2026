using Microsoft.EntityFrameworkCore;
using RADON.Database;
using RADON.Database.Models;
using RADON.Database.Models.Institutions;
using DatabaseInstitution = RADON.Database.Models.Institutions.Institution;
using ResponseInstitution = RADON.Models.Responses.Institutions.Institution;

namespace RADON.Infrastructure.Repositories.Institutions;

public interface IInstitutionRespository
{
    Task CreateOrUpdateAsync(IEnumerable<ResponseInstitution> items, CancellationToken cancellationToken = default);
}

internal class InstitutionRespository(RadonDbContext context) : IInstitutionRespository
{
    public async Task CreateOrUpdateAsync(IEnumerable<ResponseInstitution> items, CancellationToken cancellationToken = default)
    {
        if (!items.Any())
            return;

        var inputDictionary = items
            .GroupBy(k => k.InstitutionUuid)
            .ToDictionary(k => k.Key, v => v.First());
        var databaseDictionary = await context
            .Institutions
            .Include(i => i.DataSource)
            .Include(i => i.NameSnapshots)

            .Include(i => i.StatusSnapshots)
            .ThenInclude(i => i.InstitutionStatus)

            .Include(i => i.TypeSnapshots)
            .ThenInclude(i => i.InstitutionType)

            .ToDictionaryAsync(k => k.InstitutionUuid, cancellationToken);

        var inputKeys = inputDictionary.Keys.ToHashSet();
        var databaseKeys = databaseDictionary.Keys.ToHashSet();

        var existingKeys = databaseKeys.Intersect(inputKeys);
        var newKeys = inputKeys.Except(databaseKeys);

        var currentTime = DateTimeOffset.Now;
        var dataSourcesDictionary = await GetDataSourcesAsync(items, cancellationToken);
        var institutionTypeDictionary = await context
            .InstitutionTypes
            .ToDictionaryAsync(k => k.Name.ToUpperInvariant(), cancellationToken);
        var institutionStatusDictionary = await context
            .InstitutionStatuses
            .ToDictionaryAsync(k => k.Name.ToUpperInvariant(), cancellationToken);


        foreach (var key in existingKeys)
        {
            var input = inputDictionary[key];
            var database = databaseDictionary[key];
            await CreateOrUpdateAsync(input, database, currentTime, dataSourcesDictionary, institutionTypeDictionary, institutionStatusDictionary, cancellationToken);
        }

        foreach (var key in newKeys)
        {
            var input = inputDictionary[key];
            await CreateOrUpdateAsync(input, null, currentTime, dataSourcesDictionary, institutionTypeDictionary, institutionStatusDictionary, cancellationToken);
        }

        await context.SaveChangesAsync(cancellationToken);
    }

    private async Task<Dictionary<string, DataSource>> GetDataSourcesAsync(
        IEnumerable<ResponseInstitution> items,
        CancellationToken cancellationToken = default)
    {
        var databaseDictionary = await context
            .DataSources
            .ToDictionaryAsync(k => k.Name.ToUpperInvariant(), cancellationToken);

        var databaseDataSources = databaseDictionary.Keys.ToHashSet();
        var inputDataSources = items
            .Select(i => i.DataSource.ToUpperInvariant())
            .ToHashSet();

        var newDataSources = inputDataSources.Except(databaseDataSources);

        if (!newDataSources.Any())
            return databaseDictionary;

        foreach (var newDataSource in newDataSources)
        {
            var databaseItem = new DataSource
            {
                Name = newDataSource,
            };
            databaseDictionary[databaseItem.Name] = databaseItem;
            await context.DataSources.AddAsync(databaseItem, cancellationToken);
        }

        await context.SaveChangesAsync(cancellationToken);
        return databaseDictionary;
    }

    private async Task<DatabaseInstitution> CreateOrUpdateAsync(
        ResponseInstitution item,
        DatabaseInstitution? database,
        DateTimeOffset currentTime,
        Dictionary<string, DataSource> dataSourcesDictionary,
        Dictionary<string, InstitutionType> institutionTypeDictionary,
        Dictionary<string, InstitutionStatus> institutionStatusDictionary,
        CancellationToken cancellationToken = default)
    {
        database ??= new DatabaseInstitution { InstitutionUuid = item.InstitutionUuid };

        database.Regon = item.Regon;
        database.Nip = item.Nip;
        database.Krs = item.Krs;

        database.StartDate = item.StartDate;
        database.LiquidationStartDate = item.LiquidationStartDate;
        database.LiquidationDate = item.LiquidationDate;

        database.Www = item.Www;
        database.Email = item.Email;
        database.Phone = item.Phone;

        database.LastRefresh = currentTime;
        database.SourceLastRefresh = item.SourceLastRefresh;
        database.InstitutionKindCode = item.InstitutionKind.Code;
        database.DataSource = dataSourcesDictionary[item.DataSource.ToUpperInvariant()];

        await CreateOrUpdateNameSnapshotsAsync(item, database, cancellationToken);
        await CreateOrUpdateStatusSnapshotAsync(item, database, institutionStatusDictionary, cancellationToken);
        await CreateOrUpdateInstitutionTypeSnapshotAsync(item, database, institutionTypeDictionary, cancellationToken);

        return database;
    }

    private async Task CreateOrUpdateNameSnapshotsAsync(
        ResponseInstitution input,
        DatabaseInstitution database,
        CancellationToken cancellationToken = default)
    {
        var inputDictionary = input
            .Names
            .ToDictionary(k => (k.Name, k.Date));
        var databaseDictionary = database
            .NameSnapshots
            .ToDictionary(k => (k.Name, k.Date));

        var inputKeys = inputDictionary.Keys.ToHashSet();
        var databaseKeys = databaseDictionary.Keys.ToHashSet();

        var newKeys = inputKeys.Except(databaseKeys);

        if (!newKeys.Any())
            return;

        foreach (var newKey in newKeys)
        {
            var inputSnapshot = inputDictionary[newKey];
            var databaseSnapshot = new InstitutionNameSnapshot
            {
                Name = inputSnapshot.Name,
                Date = inputSnapshot.Date,
                Institution = database,
            };
            await context.InstitutionNameSnapshots.AddAsync(databaseSnapshot, cancellationToken);
        }
    }

    private async Task CreateOrUpdateStatusSnapshotAsync(
        ResponseInstitution input,
        DatabaseInstitution database,
        Dictionary<string, InstitutionStatus> institutionStatusDictionary,
        CancellationToken cancellationToken = default)
    {
        var inputDictionary = input
            .Statuses
            .ToDictionary(k => (k.Status.Name.ToUpperInvariant(), k.Date));
        var databaseDictionary = database
            .StatusSnapshots
            .ToDictionary(k => (k.InstitutionStatus.Name.ToUpperInvariant(), k.Date));

        var inputKeys = inputDictionary.Keys.ToHashSet();
        var databaseKeys = databaseDictionary.Keys.ToHashSet();

        var newKeys = inputKeys.Except(databaseKeys);

        if (!newKeys.Any())
            return;

        foreach (var (name, date) in newKeys)
        {
            var databaseSnapshot = new InstitutionStatusSnapshot
            {
                InstitutionStatus = institutionStatusDictionary[name],
                Date = date,
                Institution = database,
            };
            await context.InstitutionStatusSnapshots.AddAsync(databaseSnapshot, cancellationToken);
        }
    }

    private async Task CreateOrUpdateInstitutionTypeSnapshotAsync(
        ResponseInstitution input,
        DatabaseInstitution database,
        Dictionary<string, InstitutionType> institutionTypeDictionary,
        CancellationToken cancellationToken = default)
    {
        var inputDictionary = input
            .Types
            .ToDictionary(k => (k.Type.Name.ToUpperInvariant(), k.Date));
        var databaseDictionary = database
            .TypeSnapshots
            .ToDictionary(k => (k.InstitutionType.Name.ToUpperInvariant(), k.Date));

        var inputKeys = inputDictionary.Keys.ToHashSet();
        var databaseKeys = databaseDictionary.Keys.ToHashSet();

        var newKeys = inputKeys.Except(databaseKeys);

        if (!newKeys.Any())
            return;

        foreach (var (name, date) in newKeys)
        {
            var databaseSnapshot = new InstitutionTypeSnapshot
            {
                InstitutionType = institutionTypeDictionary[name],
                Date = date,
                Institution = database,
            };
            await context.InstitutionTypeSnapshots.AddAsync(databaseSnapshot, cancellationToken);
        }
    }
}