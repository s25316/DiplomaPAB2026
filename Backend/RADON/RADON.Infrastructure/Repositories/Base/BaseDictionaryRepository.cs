using Microsoft.EntityFrameworkCore;
using RADON.Database;
using RADON.Models.Dictionaries.Responses;

namespace RADON.Infrastructure.Repositories.Base;

public class BaseDictionaryRepository<TEntity>(
    RadonDbContext context,
    Func<RadonDbContext, CancellationToken, Task<Dictionary<string, TEntity>>> getDictionaryFunc,
    Action<TEntity, string> updateNameAction,
    Func<RadonDbContext, DbSet<TEntity>> getDbSetFunc,
    Func<DictionaryItem, TEntity> dictionaryItemToEntityFunc,
    Func<TEntity, DictionaryItem> entityToDictionaryItemFunc)
    where TEntity : class
{
    public async Task CreateOrUpdateAsync(IEnumerable<DictionaryItem> items, CancellationToken cancellationToken = default)
    {
        if (!items.Any())
            return;

        var databaseDictionary = await getDictionaryFunc(context, cancellationToken);

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
            updateNameAction(databseItem, inputItem.Name.ToUpperInvariant());
        }

        if (newKeys.Any())
        {
            foreach (var key in newKeys)
            {
                var inputItem = inputDictionary[key];
                await getDbSetFunc(context).AddAsync(dictionaryItemToEntityFunc(inputItem), cancellationToken);
            }
        }

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IDictionary<string, DictionaryItem>> GetAsync(CancellationToken cancellationToken = default) => await getDbSetFunc(context)
        .AsNoTracking()
        .Select(i => entityToDictionaryItemFunc(i))
        .ToDictionaryAsync(k => k.Code, cancellationToken);
}