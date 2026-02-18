namespace Base.Models.Interfaces.Repositories;

public interface IDictionaryRepository<TKey, TValue>
{
    Task<IDictionary<TKey, TValue>> GetAsync(CancellationToken cancellationToken = default);
}