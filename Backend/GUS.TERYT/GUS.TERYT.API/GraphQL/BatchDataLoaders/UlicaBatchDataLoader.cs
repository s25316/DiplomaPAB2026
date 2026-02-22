// Ignore Spelling: Ulica
using Base.Models.Interfaces.Repositories;
using GreenDonut;
using GUS.TERYT.Application.Repositories;
using GUS.TERYT.Models.Requests.Parameters;
using GUS.TERYT.Models.Requests.ValueObjects.Ulicy;
using GUS.TERYT.Models.Responses;

namespace GUS.TERYT.API.GraphQL.BatchDataLoaders;

public class UlicaBatchDataLoader(
        IUlicaRepository repository,
        IBatchScheduler batchScheduler,
        DataLoaderOptions options
    ) : BatchDataLoader<string, Ulica>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<string, Ulica>> LoadBatchAsync(
        IReadOnlyList<string> keys,
        CancellationToken cancellationToken)
    {
        var ids = keys.ToHashSet().Select(i => (UlicaId)i).ToList();
        var result = await repository.GetAsync(new UlicaParameters
        {
            Ids = ids,
            Pagination = new Pagination
            {
                Page = 1,
                ItemsPerPage = ids.Count,
            }
        }, cancellationToken);
        return result.Items.ToDictionary(i => i.UlicaId);
    }
}