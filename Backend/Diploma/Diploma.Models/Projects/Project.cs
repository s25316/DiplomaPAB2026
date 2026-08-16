using Diploma.Models.Dictionaries;
using Diploma.Models.Shared;

namespace Diploma.Models.Projects;

public class ProjectQueryParameters : BaseQueryParameters
{
    public enum ProjectOrderBy
    {
        Title = 1,
        CreatedAt = 2,
    }

    public required IList<Guid> ProjectIds { get; init; } = [];
    public required IList<string> Disciplines { get; init; } = [];
    public required IList<Guid> Institutions { get; init; } = [];

    public required ProjectOrderBy OrderBy { get; init; }
    public required Order Order { get; init; } = Order.Ascending;
}

public abstract record ProjectQueryResult
{
    public abstract record Failure : ProjectQueryResult
    {
        public sealed record NotFound : Failure;
        public sealed record ProfileInactive : Failure;
    };
    public sealed record Success(Response<ProjectDto> Response) : ProjectQueryResult;
}

public sealed record ProjectDto
{
    public required Guid ProjectId { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
    public required bool IsVisible { get; init; }
    public required bool IsAvailableRecruitment { get; init; }
    public required bool? IsRecruted { get; init; }

    public required IList<DictionaryItem<string>> Disciplines { get; init; } = [];
    public required IList<Guid> EductionInstitutionIds { get; init; } = [];
}