namespace RADON.Database.Models.Courses;

public class CourseForm
{
    public string CourseFormCode { get; set; } = null!;
    public string Name { get; set; } = null!;


    public virtual ICollection<CourseInstance> CourseInstances { get; set; } = [];
}