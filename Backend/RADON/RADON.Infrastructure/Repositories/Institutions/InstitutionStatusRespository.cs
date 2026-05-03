using Microsoft.EntityFrameworkCore;
using RADON.Application.Interfaces;
using RADON.Database;
using RADON.Database.Models.Institutions;
using RADON.Models.Responses.Dictionaries;

namespace RADON.Infrastructure.Repositories.Institutions;

internal class InstitutionStatusRespository(RadonDbContext context) : IInstitutionStatusRespository
{
    public async Task CreateOrUpdateAsync(IEnumerable<DictionaryItem> items, CancellationToken cancellationToken = default)
    {
        if (!items.Any())
            return;

        var databaseDictionary = await context
            .InstitutionStatuses
            .ToDictionaryAsync(k => k.InstitutionStatusCode, cancellationToken);

        var inputDictionary = items
            .GroupBy(k => k.Code)
            .ToDictionary(k => k.Key, v => v.First());

        var databaseKeys = databaseDictionary.Keys.ToHashSet();
        var inputKeys = inputDictionary.Keys.ToHashSet();

        var existingKeys = databaseKeys.Intersect(inputKeys);
        var newKeys = inputKeys.Except(databaseKeys);

        foreach (var key in existingKeys)
        {
            var databseItem = databaseDictionary[key];
            var inputItem = inputDictionary[key];
            databseItem.Name = inputItem.Name.ToUpperInvariant();
        }

        foreach (var key in newKeys)
        {
            var inputItem = inputDictionary[key];

            await context.InstitutionStatuses.AddAsync(new InstitutionStatus
            {
                InstitutionStatusCode = inputItem.Code,
                Name = inputItem.Name.ToUpperInvariant(),
            }, cancellationToken);
        }

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IDictionary<string, DictionaryItem>> GetAsync(CancellationToken cancellationToken = default) => await context
        .InstitutionStatuses
        .AsNoTracking()
        .Select(i => new DictionaryItem(i.InstitutionStatusCode, i.Name))
        .ToDictionaryAsync(k => k.Code, cancellationToken);
}