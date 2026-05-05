namespace RADON.Database.Models.Courses;

public class ProfessionalTitle
{
    public string ProfessionalTitleCode { get; set; } = null!;
    public string Name { get; set; } = null!;


    public virtual ICollection<CourseInstance> CourseInstances { get; set; } = [];
}