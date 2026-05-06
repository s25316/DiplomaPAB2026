using Microsoft.EntityFrameworkCore;
using RADON.Application.Interfaces.Base;
using RADON.Application.Interfaces.Institutions;
using RADON.Database;
using RADON.Database.Enums;
using RADON.Database.Models.Institutions;
using RADON.Models.Responses.Dictionaries;

namespace RADON.Infrastructure.Repositories.Institutions;

public abstract class InstitutionTypeRespository(RadonDbContext context, string code) : IRadonDictionaryRespository
{
    public async Task CreateOrUpdateAsync(IEnumerable<DictionaryItem> items, CancellationToken cancellationToken = default)
    {
        if (!items.Any())
            return;

        var databaseDictionary = await context
            .InstitutionTypes
            .Where(i => i.ClassificationCode == code)
            .ToDictionaryAsync(k => k.InstitutionTypeCode, cancellationToken);

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

            await context.InstitutionTypes.AddAsync(new InstitutionType
            {
                InstitutionTypeCode = inputItem.Code,
                Name = inputItem.Name.ToUpperInvariant(),
                ClassificationCode = code,
            }, cancellationToken);
        }

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IDictionary<string, DictionaryItem>> GetAsync(CancellationToken cancellationToken = default) => await context
        .InstitutionTypes
        .AsNoTracking()
        .Where(i => i.ClassificationCode == code)
        .Select(i => new DictionaryItem(i.InstitutionTypeCode, i.Name))
        .ToDictionaryAsync(k => k.Code, cancellationToken);
}

public class UniversityTypeRespository(RadonDbContext context) :
    InstitutionTypeRespository(context, ((int)InstitutionClassificationCode.UNIVERSITY).ToString()),
    IUniversityTypeRespository;

public class ScientificInstitutionTypeRespository(RadonDbContext context) :
    InstitutionTypeRespository(context, ((int)InstitutionClassificationCode.SCIENTIFIC_INSTITUTION).ToString()),
    IScientificInstitutionTypeRespository;