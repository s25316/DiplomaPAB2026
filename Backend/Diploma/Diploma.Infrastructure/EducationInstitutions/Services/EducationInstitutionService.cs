using Diploma.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace Diploma.Infrastructure.EducationInstitutions.Services;

public sealed record EducationInstitution
{
    public required Guid InstitutionUuid { get; init; }
    public required DateOnly StartDate { get; init; }
    public required DateOnly? LiquidationStartDate { get; init; }
    public required DateOnly? LiquidationDate { get; init; }
}

public interface IEducationInstitutionService
{
    IAsyncEnumerable<EducationInstitution> GetAsync(
        [EnumeratorCancellation] CancellationToken cancellationToken = default);
}

public class EducationInstitutionService(
    IHttpClientFactory factory,
    IOptions<BackendHostConfiguration> options
    ) : IEducationInstitutionService
{
    private sealed record GraphQlResponse
    {
        public sealed record Pagination
        {
            public required int ItemsPerPage { get; init; }
            public required int Page { get; init; }
            public required int TotalCount { get; init; }
        }

        public sealed record InstitutionEntry
        {
            public required IList<EducationInstitution> Items { get; init; } = [];
            public required Pagination Pagination { get; init; }
        }

        public sealed record GraphQlData
        {
            public required InstitutionEntry Institutions { get; init; }
        }

        public required GraphQlData Data { get; init; }
    }


    private const string PATH_TEMPLATE = "radon/graphql";
    private const string QUERY = """
        query GetInstitutions($page: Int!) {
          institutions(queryParameters: {
            institutionUuids: [],
            kindCodes: [],
            universityTypeCodes: [],
            scientificInstitutionTypeCodes: [],
            statusCodes: [],
            pagination: {
              itemsPerPage: 1000,
              page: $page
            }
          }) {
            items {
              institutionUuid
              startDate
              liquidationStartDate
              liquidationDate
            }
            pagination {
              itemsPerPage
              page
              totalCount
            }
          }
        }
        """;

    public async IAsyncEnumerable<EducationInstitution> GetAsync(
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var uri = GetUri();
        using var httpClient = factory.CreateClient();

        int currentPage = 1;
        bool hasMorePages = true;

        while (hasMorePages)
        {
            var payload = new
            {
                query = QUERY,
                variables = new { page = currentPage }
            };

            var response = await httpClient.PostAsJsonAsync(uri, payload, cancellationToken);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<GraphQlResponse>(cancellationToken: cancellationToken);
            ArgumentNullException.ThrowIfNull(result);

            var items = result.Data.Institutions.Items;
            var pagination = result.Data.Institutions.Pagination;

            if (items == null || items.Count == 0)
            {
                break;
            }

            foreach (var item in items)
            {
                yield return item;
            }

            if (currentPage * pagination.ItemsPerPage >= pagination.TotalCount)
            {
                hasMorePages = false;
            }
            else
            {
                currentPage++;
            }
        }
    }

    private string GetUri()
    {
        var configuration = options.Value;
        return $"{configuration.Uri.TrimEnd('/')}/{PATH_TEMPLATE}";
    }
}