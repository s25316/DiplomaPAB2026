namespace Diploma.Database.Models.Educations;

public class EducationInstitution
{
    public Guid EducationInstitutionId { get; set; }


    public virtual ICollection<EducationCourse> EducationCourses { get; set; } = [];
}