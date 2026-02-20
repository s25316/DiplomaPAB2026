// Ignore Spelling: Gmina
using Base.Models.Interfaces.Repositories;
using GreenDonut;
using GUS.TERYT.Application.Repositories;
using GUS.TERYT.Models.Requests.Parameters;
using GUS.TERYT.Models.Requests.ValueObjects.Gminy;
using GUS.TERYT.Models.Responses;

namespace GUS.TERYT.API.GraphQL.BatchDataLoaders;

public class GminaBatchDataLoader(
        IGminaRepository repository,
        IBatchScheduler batchScheduler,
        DataLoaderOptions options
    ) : BatchDataLoader<string, Gmina>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<string, Gmina>> LoadBatchAsync(
        IReadOnlyList<string> keys,
        CancellationToken cancellationToken)
    {
        var ids = keys.ToHashSet().Select(i => (GminaId)i).ToList();
        var result = await repository.GetAsync(new GminaParameters
        {
            Ids = ids,
            Pagination = new Pagination
            {
                Page = 1,
                ItemsPerPage = ids.Count,
            }
        }, cancellationToken);
        return result.Items.ToDictionary(i => $"{i.WojewodztwoCode}.{i.PowiatCode}.{i.GminaCode}{i.GminaRodzCode}");
    }
}