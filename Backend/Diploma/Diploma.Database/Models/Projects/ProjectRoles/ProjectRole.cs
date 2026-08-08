using Diploma.Database.Models.Projects.ProjectEvents;
using Diploma.Database.Models.Projects.Recruitments;

namespace Diploma.Database.Models.Projects.ProjectRoles;

public class ProjectRole
{
    public Guid ProjectRoleId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsAvailableRecruitment { get; set; } = false;


    public Guid? RootId { get; set; } = null;
    public virtual ProjectRole? Root { get; set; } = null;
    public virtual ICollection<ProjectRole> History { get; set; } = [];

    public Guid? NextId { get; set; }
    public virtual ProjectRole? Next { get; set; } = null;
    public virtual ProjectRole? Previous { get; set; } = null;

    public Guid ProjectEventId { get; set; }
    public virtual ProjectEvent ProjectEvent { get; set; } = null!;

    public virtual ICollection<ProjectRoleEducationInstitution> ProjectRoleEducationInstitutions { get; set; } = [];
    public virtual ICollection<ProjectRoleEducationCourseDiscipline> ProjectRoleEducationCourseDisciplines { get; set; } = [];
    public virtual ICollection<RecruitmentProjectRole> RecruitmentProjectRoles { get; set; } = [];
}