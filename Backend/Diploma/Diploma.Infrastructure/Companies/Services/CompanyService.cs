using Base.Models.ValueObjects.Regony;
using Diploma.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace Diploma.Infrastructure.Companies.Services;

public sealed record Company
{
    public sealed record Dates
    {
        public required DateOnly DataPowstania { get; init; }
        public required DateOnly DataRozpoczecia { get; init; }
        public required DateOnly? DataWpisu { get; init; }
        public required DateOnly? DataZawieszenia { get; init; }
        public required DateOnly? DataWznowienia { get; init; }
        public required DateOnly? DataZmiany { get; init; }
        public required DateOnly? DataZakonczenia { get; init; }
        public required DateOnly? DataSkreslenia { get; init; }
    }
    public sealed record CompanyInstitution
    {
        public required Dates Daty { get; init; }
    }

    public required string Regon { get; init; }
    public required CompanyInstitution? Institution { get; init; } = null;
}

public interface ICompanyService
{
    IAsyncEnumerable<Company> GetAsync(
        IEnumerable<Regon> regons,
        [EnumeratorCancellation] CancellationToken cancellationToken = default);
}

public class CompanyService(
    IHttpClientFactory factory,
    IOptions<BackendHostConfiguration> options
    ) : ICompanyService
{
    private sealed record GraphQlResponse
    {
        public sealed record GraphQlData
        {
            public required IList<Company> GetInstitutions { get; init; } = [];
        }

        public required GraphQlData Data { get; init; }
    }


    private const string PATH_TEMPLATE = "regon/graphql";
    private const string QUERY = """
        query GetInstitutionsQuery($regons: [Regon!]!) {
            getInstitutions(parameters: { regons: $regons }) {
                regon
                status
                institution {
                    daty {
                        dataPowstania
                        dataRozpoczecia
                        dataWpisu
                        dataZawieszenia
                        dataWznowienia
                        dataZmiany
                        dataZakonczenia
                        dataSkreslenia
                    }
                }
            }
        }
        """;


    public async IAsyncEnumerable<Company> GetAsync(
        IEnumerable<Regon> regons,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var uri = GetUri();
        using var httpClient = factory.CreateClient();

        var x = regons.Select(i => i.To14SCharacters()).ToArray();
        var payload = new
        {
            query = QUERY,
            variables = new
            {
                regons = x,
            }
        };

        var response = await httpClient.PostAsJsonAsync(uri, payload, cancellationToken);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<GraphQlResponse>(cancellationToken: cancellationToken);
        ArgumentNullException.ThrowIfNull(result);

        var items = result.Data.GetInstitutions;

        foreach (var item in items)
        {
            yield return item;
        }
    }

    private string GetUri()
    {
        var configuration = options.Value;
        return $"{configuration.Uri.TrimEnd('/')}/{PATH_TEMPLATE}";
    }
}