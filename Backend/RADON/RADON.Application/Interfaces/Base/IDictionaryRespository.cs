namespace RADON.Application.Interfaces.Base;

public interface IDictionaryRespository<TKey, TItem>
{
    Task<IDictionary<TKey, TItem>> GetAsync(CancellationToken cancellationToken = default);
    Task CreateOrUpdateAsync(IEnumerable<TItem> items, CancellationToken cancellationToken = default);
}