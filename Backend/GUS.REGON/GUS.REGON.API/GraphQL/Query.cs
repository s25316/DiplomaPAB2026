using GUS.REGON.Application.Interfaces;
using GUS.REGON.Models.Requests;
using HotChocolate;
using RegonResult = GUS.REGON.Models.Responses.Result;

namespace GUS.REGON.API.GraphQL;

public class Query
{
    [GraphQLName("getReports")]
    public async Task<IEnumerable<RegonResult>> GetWojewodztwaAsync(
        [Service] IReportRepository reportRepository,
        InputParameters parameters,
        CancellationToken cancellationToken
    ) => await reportRepository.GetAsync(parameters, cancellationToken);
}
