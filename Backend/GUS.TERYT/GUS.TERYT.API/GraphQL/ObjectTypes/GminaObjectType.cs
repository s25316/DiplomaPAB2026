// Ignore Spelling: Powiat, Gmina
using GUS.TERYT.API.GraphQL.BatchDataLoaders;
using GUS.TERYT.Models.Responses;
using HotChocolate;
using HotChocolate.Types;

namespace GUS.TERYT.API.GraphQL.ObjectTypes;

public class GminaObjectType : ObjectType<Gmina>
{
    protected override void Configure(IObjectTypeDescriptor<Gmina> descriptor)
    {
        descriptor.Field("powiat")
            .ResolveWith<GminaResolvers>(r => r.GetPowiatAsync(default!, default!, default!));
    }

    private sealed class GminaResolvers
    {
        public async Task<Powiat?> GetPowiatAsync(
            [Parent] Gmina gmina,
            PowiatBatchDataLoader dataLoader,
            CancellationToken cancellationToken)
        {
            return await dataLoader.LoadAsync($"{gmina.WojewodztwoCode}.{gmina.PowiatCode}", cancellationToken);
        }
    }
}