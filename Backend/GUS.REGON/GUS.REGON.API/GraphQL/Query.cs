using GUS.REGON.Application.Interfaces;
using GUS.REGON.Models;
using GUS.REGON.Models.Responses;
using HotChocolate;

namespace GUS.REGON.API.GraphQL;

public class Query
{
    [GraphQLName("getReports")]
    public async Task<IEnumerable<Report.Full>> GetWojewodztwaAsync(
        [Service] IReportRepository reportRepository,
        QueryParameters parameters,
        CancellationToken cancellationToken
    ) => await reportRepository.GetAsync(parameters, cancellationToken);
}