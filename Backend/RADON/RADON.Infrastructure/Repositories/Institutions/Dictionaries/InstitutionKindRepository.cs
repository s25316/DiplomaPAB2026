using Microsoft.EntityFrameworkCore;
using RADON.Application.Interfaces.Institutions.Dictionaries;
using RADON.Database;
using RADON.Database.Models.Institutions;
using RADON.Models.Responses.Dictionaries;

namespace RADON.Infrastructure.Repositories.Institutions.Dictionaries;

public class InstitutionKindRepository(RadonDbContext context) : IInstitutionKindRepository
{
    public async Task CreateOrUpdateAsync(IEnumerable<DictionaryItem> items, CancellationToken cancellationToken = default)
    {
        if (!items.Any())
            return;

        var databaseDictionary = await context
            .InstitutionKinds
            .ToDictionaryAsync(k => k.InstitutionKindCode, cancellationToken);

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

        if (newKeys.Any())
        {
            var classifications = await context
                .InstitutionClassifications
                .ToListAsync(cancellationToken);

            foreach (var key in newKeys)
            {
                var inputItem = inputDictionary[key];
                var classification = classifications.First(i => inputItem
                    .Name
                    .ToUpperInvariant()
                    .Contains(i.Name.ToUpperInvariant())
                );

                await context.InstitutionKinds.AddAsync(new InstitutionKind
                {
                    InstitutionKindCode = inputItem.Code,
                    Name = inputItem.Name.ToUpperInvariant(),
                    ClassificationCode = classification.InstitutionClassificationCode,
                }, cancellationToken);
            }
        }

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IDictionary<string, DictionaryItem>> GetAsync(CancellationToken cancellationToken = default) => await context
        .InstitutionKinds
        .AsNoTracking()
        .Select(i => new DictionaryItem(i.InstitutionKindCode, i.Name))
        .ToDictionaryAsync(k => k.Code, cancellationToken);
}