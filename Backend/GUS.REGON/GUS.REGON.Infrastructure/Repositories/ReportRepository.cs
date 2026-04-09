using Base.Models.Interfaces.Repositories;
using GUS.REGON.Application.Interfaces;
using GUS.REGON.Models.Requests;
using GUS.REGON.Models.Responses;

namespace GUS.REGON.Infrastructure.Repositories;

public class ReportRepository : IReportRepository
{
    public async Task<Response<Result>.ManyItems> GetAsync(InputParameters parameters, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}