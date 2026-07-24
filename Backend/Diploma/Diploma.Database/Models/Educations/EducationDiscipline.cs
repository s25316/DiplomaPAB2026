namespace Diploma.Database.Models.Educations;

public class EducationDiscipline
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;


    public virtual ICollection<EducationCourseDiscipline> EducationCourseDisciplines { get; set; } = [];
}