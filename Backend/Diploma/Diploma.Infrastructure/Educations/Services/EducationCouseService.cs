using Diploma.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace Diploma.Infrastructure.Educations.Services;

public sealed record EducationCourse
{
    public sealed record Discipline
    {
        public required string Code { get; init; }
    }

    public sealed record CourseDiscipline
    {
        public required int Percentage { get; init; }
        public required bool IsLeading { get; init; }
        public required Discipline Discipline { get; init; }
    }

    public sealed record Instance
    {
        public required Guid CourseInstanceUuid { get; init; }
        public required DateOnly EducationStartDate { get; init; }
        public required DateOnly? LiquidationDate { get; init; }
    }


    public required Guid CourseUuid { get; init; }
    public required Guid InstitutionUuid { get; init; }
    public required DateOnly? CreationDate { get; init; }
    public required DateOnly? TerminationInitializationDate { get; init; }
    public required DateOnly? LiquidationDate { get; init; }
    public required IEnumerable<CourseDiscipline> Disciplines { get; init; } = [];
    public required IEnumerable<Instance> CourseInstances { get; init; } = [];
}


public interface IEducationCouseService
{
    IAsyncEnumerable<EducationCourse> GetAsync([EnumeratorCancellation] CancellationToken cancellationToken = default);
}

public class EducationCouseService(
    IHttpClientFactory factory,
    IOptions<BackendHostConfiguration> options
    ) : IEducationCouseService
{
    private sealed record GraphQlResponse
    {
        public sealed record Pagination
        {
            public required int ItemsPerPage { get; init; }
            public required int Page { get; init; }
            public required int TotalCount { get; init; }
        }

        public sealed record CourseEntry
        {
            public required IList<EducationCourse> Items { get; init; } = [];
            public required Pagination Pagination { get; init; }
        }

        public sealed record GraphQlData
        {
            public required CourseEntry Courses { get; init; }
        }

        public required GraphQlData Data { get; init; }
    }


    private const string PATH_TEMPLATE = "radon/graphql";
    private const string QUERY = """
        query GetCourses($page: Int!){
          courses(queryParameters:  {
            courseInstanceUuids: [],
            institutionUuids: [],
            statusCodes: [],
            courseUuids: [],
            levelCodes: [],
            iscedCodes: [],
            disciplineCodes: [],
            profileCodes: [],
            formCodes: [],
            professionalTitleCodes: [],
            languageCodes: [],
            instanceStatusCodes: [],
            philologicalLanguageCodes: [],
             pagination:  {
                itemsPerPage: 1000,
                page: $page,
             }
          })
          {
            items
            {
              courseUuid,
              institutionUuid,
              creationDate,
              terminationInitializationDate,
              liquidationDate,
              disciplines{
                percentage,
                isLeading,
                discipline{
                  code,
                }
              },
              courseInstances
              {
                  courseInstanceUuid,
                  educationStartDate,
                  liquidationDate,
              },
            },
            pagination{
              itemsPerPage,
              page,
              totalCount
            }
          }
        }
        """;


    public async IAsyncEnumerable<EducationCourse> GetAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
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

            var items = result.Data.Courses.Items;
            var pagination = result.Data.Courses.Pagination;

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