using Microsoft.EntityFrameworkCore;
using RADON.Application.Interfaces.Base;
using RADON.Application.Interfaces.Institutions.Dictionaries;
using RADON.Database;
using RADON.Database.Enums;
using RADON.Database.Models.Institutions;
using RADON.Models.Responses.Dictionaries;

namespace RADON.Infrastructure.Repositories.Institutions.Dictionaries;

public abstract class InstitutionTypeRepository(RadonDbContext context, string code) : IRadonDictionaryRepository
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

public class UniversityTypeRepository(RadonDbContext context) :
    InstitutionTypeRepository(context, ((int)InstitutionClassificationCode.UNIVERSITY).ToString()),
    IUniversityTypeRepository;

public class ScientificInstitutionTypeRepository(RadonDbContext context) :
    InstitutionTypeRepository(context, ((int)InstitutionClassificationCode.SCIENTIFIC_INSTITUTION).ToString()),
    IScientificInstitutionTypeRepository;