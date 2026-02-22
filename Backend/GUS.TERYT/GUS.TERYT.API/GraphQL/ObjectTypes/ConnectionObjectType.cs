// Ignore Spelling: miejscowosc, ulica
using GUS.TERYT.API.GraphQL.BatchDataLoaders;
using GUS.TERYT.Models.Responses;
using HotChocolate;
using HotChocolate.Types;

namespace GUS.TERYT.API.GraphQL.ObjectTypes;

public class ConnectionObjectType : ObjectType<Connection>
{
    protected override void Configure(IObjectTypeDescriptor<Connection> descriptor)
    {
        descriptor.Name("connection");
        descriptor.Field("miejscowosc")
            .ResolveWith<ConnectionResolvers>(r => r.GetMiejscowoscAsync(default!, default!, default!));

        descriptor.Field("ulica")
            .ResolveWith<ConnectionResolvers>(r => r.GetUlicaAsync(default!, default!, default!));
    }

    private sealed class ConnectionResolvers
    {
        [Serial]
        public async Task<Miejscowosc?> GetMiejscowoscAsync(
            [Parent] Connection connection,
            MiejscowoscBatchDataLoader dataLoader,
            CancellationToken cancellationToken)
        {
            return await dataLoader.LoadAsync(connection.MiejscowoscId, cancellationToken);
        }

        [Serial]
        public async Task<Ulica?> GetUlicaAsync(
            [Parent] Connection connection,
            UlicaBatchDataLoader dataLoader,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(connection.UlicaId))
            {
                return null;
            }
            return await dataLoader.LoadAsync(connection.UlicaId, cancellationToken);
        }
    }
}