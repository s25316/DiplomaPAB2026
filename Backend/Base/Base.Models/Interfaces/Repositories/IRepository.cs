namespace Base.Models.Interfaces.Repositories;

public interface IRepository<in TParameters, TItem>
{
    Task<Response<TItem>> GetAsync(TParameters parameters, CancellationToken cancellationToken = default);
}