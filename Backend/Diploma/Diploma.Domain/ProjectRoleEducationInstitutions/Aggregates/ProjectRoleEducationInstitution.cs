using Diploma.Domain.Base.Aggregates;
using Diploma.Domain.EducationInstitutions.Aggregates;
using Diploma.Domain.ProjectRoles.Aggregates;
using Diploma.Domain.Projects.Aggregates;

namespace Diploma.Domain.ProjectRoleEducationInstitutions.Aggregates;

public record ProjectRoleEducationInstitutionId : BaseEntityId<Guid>
{
    public static implicit operator Guid(ProjectRoleEducationInstitutionId value) => value.Value;
    public static implicit operator ProjectRoleEducationInstitutionId(Guid value) => new() { Value = value };
}

public partial class ProjectRoleEducationInstitution : BaseEntity<ProjectRoleEducationInstitutionId>
{
    public ProjectId ProjectId { get; private set; } = null!;
    public ProjectRoleId ProjectRoleId { get; private set; } = null!;
    public EducationInstitutionId EducationInstitutionId { get; private set; } = null!;
    public DateTimeOffset CreatedAt { get; private set; }


    public static ProjectRoleEducationInstitution Create(
        ProjectId projectId,
        ProjectRoleId projectRoleId,
        EducationInstitutionId educationInstitutionId)
    {
        var item = new ProjectRoleEducationInstitution();

        item.ProjectId = projectId;
        item.ProjectRoleId = projectRoleId;
        item.EducationInstitutionId = educationInstitutionId;
        item.CreatedAt = DateTimeOffset.Now;

        return item;
    }
}