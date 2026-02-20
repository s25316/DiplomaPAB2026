// Ignore Spelling: Wojewodztwo, Powiat
using GUS.TERYT.API.GraphQL.BatchDataLoaders;
using GUS.TERYT.Models.Responses;
using HotChocolate;
using HotChocolate.Types;

namespace GUS.TERYT.API.GraphQL.ObjectTypes;

public class PowiatObjectType : ObjectType<Powiat>
{
    protected override void Configure(IObjectTypeDescriptor<Powiat> descriptor)
    {
        descriptor.Field("wojewodztwo")
            .ResolveWith<PowiatResolvers>(r => r.GetWojewodztwoAsync(default!, default!, default!));
    }

    private sealed class PowiatResolvers
    {
        public async Task<Wojewodztwo?> GetWojewodztwoAsync(
            [Parent] Powiat powiat,
            WojewodztwoBatchDataLoader dataLoader,
            CancellationToken cancellationToken)
        {
            return await dataLoader.LoadAsync(powiat.WojewodztwoCode, cancellationToken);
        }
    }
}