using Diploma.Models.Dictionaries;
using Diploma.Models.ProjectRoles;
using Diploma.Models.Projects;
using Diploma.Models.Shared;

namespace Diploma.Models.Recruitments;

public sealed class RecruitmentQueryParameters : BaseQueryParameters
{
    public enum RecruitmentOrderBy
    {
        Title = 1,
        CreatedAt = 2,
    }

    public required int StatusId { get; set; }

    public required RecruitmentOrderBy OrderBy { get; init; }
    public required Order Order { get; init; }
}

public sealed record RecruitmentDto
{
    public required Guid RecruitmentId { get; init; }
    public required DictionaryItem<int> Status { get; init; }
    public required ProjectDto Project { get; init; }
    public required IEnumerable<ProjectRoleDto> ProjectRoles { get; init; }
}