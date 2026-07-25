using Diploma.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Diploma.Infrastructure.EducationDisciplines.Services;

public sealed record EducationDiscipline
{
    public required string Code { get; init; }
    public required string Name { get; init; }
}

public interface IEducationDisciplineService
{
    Task<IEnumerable<EducationDiscipline>> GetAsync(CancellationToken cancellationToken = default);
}

public class EducationDisciplineService(
    IHttpClientFactory factory,
    IOptions<BackendHostConfiguration> options
    ) : IEducationDisciplineService
{
    private sealed record GraphQlResponse
    {
        public sealed record DisciplineEntry
        {
            public required string Key { get; init; }
            public required EducationDiscipline Value { get; init; }
        }

        public sealed record GraphQlData
        {
            public required List<DisciplineEntry> Disciplines { get; init; } = [];
        }

        public required GraphQlData Data { get; init; }
    }


    private const string PATH_TEMPLATE = "radon/graphql";
    private const string QUERY = "query { disciplines { key, value { code, name } } }";


    public async Task<IEnumerable<EducationDiscipline>> GetAsync(CancellationToken cancellationToken = default)
    {
        var uri = GetUri();
        using var httpClient = factory.CreateClient();
        var response = await httpClient.PostAsJsonAsync(uri, new { query = QUERY }, cancellationToken);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<GraphQlResponse>(cancellationToken: cancellationToken);
        ArgumentNullException.ThrowIfNull(result);

        return result.Data.Disciplines.Select(i => i.Value);
    }

    private string GetUri()
    {
        var configuration = options.Value;
        return $"{configuration.Uri.TrimEnd('/')}/{PATH_TEMPLATE}";
    }
}