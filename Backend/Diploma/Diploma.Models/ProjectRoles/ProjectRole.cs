using Diploma.Models.Dictionaries;
using Diploma.Models.Shared;

namespace Diploma.Models.ProjectRoles;

public class ProjectRoleQueryParameters : BaseQueryParameters
{
    public enum ProjectRoleOrderBy
    {
        Title = 1,
        CreatedAt = 2,
    }

    public required IList<Guid> ProjectRoleIds { get; init; } = [];
    public required IList<Guid> ProjectIds { get; init; } = [];
    public required IList<string> Disciplines { get; init; } = [];
    public required IList<Guid> Institutions { get; init; } = [];

    public required ProjectRoleOrderBy OrderBy { get; init; }
    public required Order Order { get; init; } = Order.Ascending;
}

public abstract record ProjectRoleQueryResult
{
    public abstract record Failure : ProjectRoleQueryResult
    {
        public sealed record NotFound : Failure;
        public sealed record ProfileInactive : Failure;
    };
    public sealed record Success(Response<ProjectRoleDto> Response) : ProjectRoleQueryResult;
}

public sealed record ProjectRoleDto
{
    public sealed record ProjectRoleDiscipline
    {
        public required Guid ProjectRoleDisciplineId { get; init; }
        public required DictionaryItem<string> Discipline { get; init; }
    }

    public sealed record ProjectRoleEductionInstitution
    {
        public required Guid ProjectRoleEductionInstitutionId { get; init; }
        public required Guid EductionInstitutionId { get; init; }
    }

    public required Guid ProjectRoleId { get; init; }
    public required Guid ProjectId { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
    public required bool IsAvailableRecruitment { get; init; }
    public required bool? IsRecruted { get; init; }

    public required IList<ProjectRoleDiscipline> Disciplines { get; init; } = [];
    public required IList<ProjectRoleEductionInstitution> EductionInstitutionIds { get; init; } = [];
}