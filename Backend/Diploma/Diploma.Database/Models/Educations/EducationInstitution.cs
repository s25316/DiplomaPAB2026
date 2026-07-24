namespace Diploma.Database.Models.Educations;

public class EducationInstitution
{
    public Guid EducationInstitutionId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? LiquidationStartDate { get; set; } = null;
    public DateOnly? LiquidationDate { get; set; } = null;


    public virtual ICollection<EducationCourse> EducationCourses { get; set; } = [];
}