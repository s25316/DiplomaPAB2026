using Diploma.Database.Models.Projects.ProjectRoles;

namespace Diploma.Database.Models.Educations;

public class EducationCourseDiscipline
{
    public Guid EducationCourseDisciplineId { get; set; }
    public int Percentage { get; set; }
    public bool IsLeading { get; set; }


    public Guid EducationCourseId { get; set; }
    public virtual EducationCourse EducationCourse { get; set; } = null!;

    public string EducationDisciplineCode { get; set; } = null!;
    public virtual EducationDiscipline EducationDiscipline { get; set; } = null!;

    public virtual ICollection<ProjectRoleEducationCourseDiscipline> ProjectRoleEducationCourseDisciplines { get; set; } = [];
}