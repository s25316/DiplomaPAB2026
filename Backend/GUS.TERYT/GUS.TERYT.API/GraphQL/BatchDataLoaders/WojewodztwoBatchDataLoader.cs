// Ignore spelling: Wojewodztwo
using Base.Models.Interfaces.Repositories;
using GreenDonut;
using GUS.TERYT.Application.Repositories;
using GUS.TERYT.Models.Requests.Parameters;
using GUS.TERYT.Models.Requests.ValueObjects.Wojewodztwa;
using GUS.TERYT.Models.Responses;

namespace GUS.TERYT.API.GraphQL.BatchDataLoaders;

public class WojewodztwoBatchDataLoader(
        IWojewodztwoRepository repository,
        IBatchScheduler batchScheduler,
        DataLoaderOptions options
    ) : BatchDataLoader<string, Wojewodztwo>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<string, Wojewodztwo>> LoadBatchAsync(
        IReadOnlyList<string> keys,
        CancellationToken cancellationToken)
    {
        var ids = keys.ToHashSet().Select(i => (WojewodztwoId)i).ToList();
        var result = await repository.GetAsync(new WojewodztwoParameters
        {
            Ids = ids,
            Pagination = new Pagination
            {
                Page = 1,
                ItemsPerPage = ids.Count,
            }
        }, cancellationToken);
        return result.Items.ToDictionary(x => x.WojewodztwoCode);
    }
}