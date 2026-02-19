// Ignore spelling: api, Teryt, wojewodztwa, Powiaty, Gminy, Miejscowosci, Ulicy
using GUS.TERYT.Application.Repositories;
using GUS.TERYT.Models.Responses;
using HotChocolate;
using HotChocolate.Types;

namespace GUS.TERYT.API.GraphQL;

[ExtendObjectType("Query")]
public class Dictionaries
{
    [GraphQLName("getPowiatyTypes")]
    public async Task<IEnumerable<Powiat.Type>> GetPowiatyTypesAsync(
        [Service] IPowiatTypeRepository repository,
        CancellationToken cancellationToken)
    {
        var items = await repository.GetAsync(cancellationToken);
        return items.Values;
    }


    [GraphQLName("getGminyTypes")]
    public async Task<IEnumerable<Gmina.Type>> GetGminyTypesAsync(
        [Service] IGminaTypeRepository repository,
        CancellationToken cancellationToken)
    {
        var items = await repository.GetAsync(cancellationToken);
        return items.Values;
    }


    [GraphQLName("getMiejscowosciTypes")]
    public async Task<IEnumerable<Miejscowosc.Type>> GetMiejscowosciTypesAsync(
        [Service] IMiejscowoscTypeRepository repository,
        CancellationToken cancellationToken)
    {
        var items = await repository.GetAsync(cancellationToken);
        return items.Values;
    }


    [GraphQLName("getUlicyTypes")]
    public async Task<IEnumerable<Ulica.Type>> GetUlicyTypesAsync(
        [Service] IUlicaTypeRepository repository,
        CancellationToken cancellationToken)
    {
        var items = await repository.GetAsync(cancellationToken);
        return items.Values;
    }
}