using Diploma.Domain.Base.Aggregates;
using Diploma.Domain.Projects.Aggregates;

namespace Diploma.Domain.ProjectRoles.Aggregates;

public record ProjectRoleId : BaseEntityId<Guid>
{
    public static implicit operator Guid(ProjectRoleId value) => value.Value;
    public static implicit operator ProjectRoleId(Guid value) => new() { Value = value };
}
public partial class ProjectRole : BaseEntity<ProjectRoleId>
{
    public ProjectId ProjectId { get; protected set; } = null!;
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsAvailableRecruitment { get; protected set; } = false;
    public DateTimeOffset CreatedAt { get; protected set; } = DateTimeOffset.Now;


    public void ChangeAvailableRecruitment(bool? value)
    {
        IsAvailableRecruitment = value ?? !IsAvailableRecruitment;
    }
}