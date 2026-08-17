using Diploma.Models.Dictionaries;
using Diploma.Models.ProjectRoles;
using Diploma.Models.Projects;

namespace Diploma.Models.Recruitments;

internal class Recruitment
{
}

public sealed record RecruitmentDto
{
    public required Guid RecruitmentId { get; init; }
    public required DictionaryItem<int> Status { get; init; }
    public required ProjectDto Project { get; init; }
    public required IEnumerable<ProjectRoleDto> ProjectRoles { get; init; }
}