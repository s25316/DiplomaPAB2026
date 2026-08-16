using Diploma.Domain.Base.Aggregates;
using Diploma.Domain.ProjectRoles.Aggregates;
using Diploma.Domain.Projects.Aggregates;

namespace Diploma.Domain.ProjectRoleDisciplines.Aggregates;

public record ProjectRoleDisciplineId : BaseEntityId<Guid>
{
    public static implicit operator Guid(ProjectRoleDisciplineId value) => value.Value;
    public static implicit operator ProjectRoleDisciplineId(Guid value) => new() { Value = value };
}

public partial class ProjectRoleDiscipline : BaseEntity<ProjectRoleDisciplineId>
{
    public ProjectId ProjectId { get; private set; } = null!;
    public ProjectRoleId ProjectRoleId { get; private set; } = null!;
    public string DisciplineCode { get; private set; } = null!;
    public DateTimeOffset CreatedAt { get; private set; }


    public static ProjectRoleDiscipline Create(
        ProjectId projectId,
        ProjectRoleId projectRoleId,
        string DisciplineCode)
    {
        var item = new ProjectRoleDiscipline();

        item.ProjectId = projectId;
        item.ProjectRoleId = projectRoleId;
        item.DisciplineCode = DisciplineCode;
        item.CreatedAt = DateTimeOffset.Now;

        return item;
    }
}