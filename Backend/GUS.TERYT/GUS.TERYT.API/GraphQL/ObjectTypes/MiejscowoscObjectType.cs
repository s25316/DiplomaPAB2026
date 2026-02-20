// Ignore Spelling: Gmina, Miejscowosc
using GUS.TERYT.API.GraphQL.BatchDataLoaders;
using GUS.TERYT.Models.Responses;
using HotChocolate;
using HotChocolate.Types;

namespace GUS.TERYT.API.GraphQL.ObjectTypes;

public class MiejscowoscObjectType : ObjectType<Miejscowosc>
{
    protected override void Configure(IObjectTypeDescriptor<Miejscowosc> descriptor)
    {
        descriptor.Field("gmina")
            .ResolveWith<MiejscowoscResolvers>(r => r.GetGminaAsync(default!, default!, default!));
    }

    private sealed class MiejscowoscResolvers
    {
        public async Task<Gmina?> GetGminaAsync(
            [Parent] Miejscowosc miejscowosc,
            GminaBatchDataLoader dataLoader,
            CancellationToken cancellationToken)
        {
            return await dataLoader.LoadAsync($"{miejscowosc.WojewodztwoCode}.{miejscowosc.PowiatCode}.{miejscowosc.GminaCode}{miejscowosc.GminaRodzCode}", cancellationToken);
        }
    }
}