using Diploma.Database.Models.Projects.Recruitments;

namespace Diploma.Database.Models.Projects.ProjectRoles;

public class ProjectRole
{
    public Guid ProjectRoleId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? RemovedAt { get; set; }


    public Guid ProjectId { get; set; }
    public virtual Project Project { get; set; } = null!;

    public Guid? LastProjectRoleDataId { get; set; }
    public virtual ProjectRoleData? LastProjectRoleData { get; set; } = null;

    public virtual ICollection<ProjectRoleData> ProjectRoleDatas { get; set; } = [];
    public virtual ICollection<ProjectRoleEducationInstitution> ProjectRoleEducationInstitutions { get; set; } = [];
    public virtual ICollection<ProjectRoleEducationDiscipline> ProjectRoleEducationCourseDisciplines { get; set; } = [];
    public virtual ICollection<RecruitmentProjectRole> RecruitmentProjectRoles { get; set; } = [];
}