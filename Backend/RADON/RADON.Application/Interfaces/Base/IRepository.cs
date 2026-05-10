using RADON.Models.Shared;

namespace RADON.Application.Interfaces.Base;

public interface IRepository<TItem, TQueryParameters>
    where TItem : class
    where TQueryParameters : BaseQueryParameters
{
    Task<Response<TItem>> GetAsync(TQueryParameters parameters, CancellationToken cancellationToken = default);
    Task CreateOrUpdateAsync(IEnumerable<TItem> items, CancellationToken cancellationToken = default);
}