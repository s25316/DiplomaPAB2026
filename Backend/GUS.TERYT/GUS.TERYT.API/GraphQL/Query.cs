// Ignore spelling: wojewodztwa, Powiaty, Gminy, Miejscowosci, Ulicy
using AppAny.HotChocolate.FluentValidation;
using Base.Models.Interfaces.Repositories;
using GUS.TERYT.Application.Repositories;
using GUS.TERYT.Models.Requests.Parameters;
using GUS.TERYT.Models.Requests.ValueObjects;
using GUS.TERYT.Models.Responses;
using HotChocolate;

namespace GUS.TERYT.API.GraphQL;

public class Query
{
    [GraphQLName("getWojewodztwa")]
    public async Task<Response<Wojewodztwo>.ManyItems> GetWojewodztwaAsync(
        [Service] IWojewodztwoRepository repository,
        [UseFluentValidation] WojewodztwoParameters parameters,
        CancellationToken cancellationToken
    ) => await repository.GetAsync(parameters, cancellationToken);


    [GraphQLName("getPowiaty")]
    public async Task<Response<Powiat>.ManyItems> GetPowiatyAsync(
        [Service] IPowiatRepository repository,
        [UseFluentValidation] PowiatParameters parameters,
        CancellationToken cancellationToken
    ) => await repository.GetAsync(parameters, cancellationToken);


    [GraphQLName("getGminy")]
    public async Task<Response<Gmina>.ManyItems> GetGminyAsync(
        [Service] IGminaRepository repository,
        [UseFluentValidation] GminaParameters parameters,
        CancellationToken cancellationToken
    ) => await repository.GetAsync(parameters, cancellationToken);


    [GraphQLName("getMiejscowosci")]
    public async Task<Response<Miejscowosc>.ManyItems> GetMiejscowosciAsync(
        [Service] IMiejscowoscRepository repository,
        [UseFluentValidation] MiejscowoscParameters parameters,
        CancellationToken cancellationToken
    ) => await repository.GetAsync(parameters, cancellationToken);


    [GraphQLName("getUlicy")]
    public async Task<Response<Ulica>.ManyItems> GetUlicyAsync(
        [Service] IUlicaRepository repository,
        [UseFluentValidation] UlicaParameters parameters,
        CancellationToken cancellationToken
    ) => await repository.GetAsync(parameters, cancellationToken);


    [GraphQLName("getConnection")]
    public async Task<Response<Ulica>.ManyItems> GetUlicyAsync(
        [UseFluentValidation] IList<MiejscowoscUlicaKeys> keys,
        CancellationToken cancellationToken
    ) => /// TODO
}
