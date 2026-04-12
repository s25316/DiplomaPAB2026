using GUS.REGON.Models.Requests;
using GUS.REGON.Models.Responses;

namespace GUS.REGON.Application.Interfaces;

public interface IReportRepository
{
    Task<IEnumerable<Result>> GetAsync(InputParameters parameters, CancellationToken cancellationToken = default);
}