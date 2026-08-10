using Diploma.Database.Models.Educations;
using Diploma.Database.Models.Projects.ProjectEvents;

namespace Diploma.Database.Models.Projects.ProjectRoles;

public class ProjectRoleEducationDiscipline
{
    public Guid ProjectRoleEducationDisciplineId { get; set; }


    public Guid ProjectRoleId { get; set; }
    public virtual ProjectRole ProjectRole { get; set; } = null!;

    public string EducationDisciplineCode { get; set; } = null!;
    public virtual EducationDiscipline EducationDiscipline { get; set; } = null!;

    public Guid AddProjectEventId { get; set; }
    public virtual ProjectEvent AddProjectEvent { get; set; } = null!;

    public Guid? RemoveProjectEventId { get; set; }
    public virtual ProjectEvent? RemoveProjectEvent { get; set; } = null!;
}