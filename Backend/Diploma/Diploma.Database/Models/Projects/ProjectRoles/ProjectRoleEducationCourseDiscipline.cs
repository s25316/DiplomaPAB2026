using Diploma.Database.Models.Educations;
using Diploma.Database.Models.Projects.ProjectEvents;

namespace Diploma.Database.Models.Projects.ProjectRoles;

public class ProjectRoleEducationCourseDiscipline
{
    public Guid ProjectRoleEducationCourseDisciplineId { get; set; }


    public Guid ProjectRoleId { get; set; }
    public virtual ProjectRole ProjectRole { get; set; } = null!;

    public Guid EducationCourseDisciplineId { get; set; }
    public virtual EducationCourseDiscipline EducationCourseDiscipline { get; set; } = null!;

    public Guid AddProjectEventId { get; set; }
    public virtual ProjectEvent AddProjectEvent { get; set; } = null!;

    public Guid? RemoveProjectEventId { get; set; }
    public virtual ProjectEvent? RemoveProjectEvent { get; set; } = null!;
}