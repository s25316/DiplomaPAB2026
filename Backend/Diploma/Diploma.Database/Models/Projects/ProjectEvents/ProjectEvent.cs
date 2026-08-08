using Diploma.Database.Models.Persons;
using Diploma.Database.Models.Projects.ProjectManagers;
using Diploma.Database.Models.Projects.ProjectRoles;

namespace Diploma.Database.Models.Projects.ProjectEvents;

public class ProjectEvent
{
    public Guid ProjectEventId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }


    public int ProjectEventTypeId { get; set; }
    public virtual ProjectEventType ProjectEventType { get; set; } = null!;

    public Guid PersonId { get; set; }
    public virtual Person Person { get; set; } = null!;

    public Guid ProjectId { get; set; }
    public virtual Project Project { get; set; } = null!;


    public virtual ProjectManager? GrantProjectManager { get; set; } = null;
    public virtual ProjectManager? RevokeProjectManager { get; set; } = null;
    public virtual ProjectRole? ProjectRole { get; set; } = null;
    public virtual ProjectRoleEducationCourseDiscipline? AddProjectRoleEducationCourseDiscipline { get; set; } = null;
    public virtual ProjectRoleEducationCourseDiscipline? RemoveProjectRoleEducationCourseDiscipline { get; set; } = null;
    public virtual ProjectRoleEducationInstitution? AddProjectRoleEducationInstitution { get; set; } = null;
    public virtual ProjectRoleEducationInstitution? RemoveProjectRoleEducationInstitution { get; set; } = null;
}