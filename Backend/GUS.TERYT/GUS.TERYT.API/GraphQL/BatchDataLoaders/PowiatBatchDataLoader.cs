// Ignore Spelling: Powiat
using Base.Models.Interfaces.Repositories;
using GreenDonut;
using GUS.TERYT.Application.Repositories;
using GUS.TERYT.Models.Requests.Parameters;
using GUS.TERYT.Models.Requests.ValueObjects.Powiaty;
using GUS.TERYT.Models.Responses;

namespace GUS.TERYT.API.GraphQL.BatchDataLoaders;

public class PowiatBatchDataLoader(
        IPowiatRepository repository,
        IBatchScheduler batchScheduler,
        DataLoaderOptions options
    ) : BatchDataLoader<string, Powiat>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<string, Powiat>> LoadBatchAsync(
        IReadOnlyList<string> keys,
        CancellationToken cancellationToken)
    {
        var ids = keys.ToHashSet().Select(i => (PowiatId)i).ToList();
        var result = await repository.GetAsync(new PowiatParameters
        {
            Ids = ids,
            Pagination = new Pagination
            {
                Page = 1,
                ItemsPerPage = ids.Count,
            }
        }, cancellationToken);
        return result.Items.ToDictionary(i => $"{i.WojewodztwoCode}.{i.PowiatCode}");
    }
}