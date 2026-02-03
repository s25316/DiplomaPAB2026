using GUS.TERYT.Models;

namespace GUS.TERYT.Application.Repositories;

public interface IBaseRepository<in TParameters, TItem>
{
    Task<Response<TItem>> GetAsync(TParameters parameters, CancellationToken cancellationToken = default);
}
