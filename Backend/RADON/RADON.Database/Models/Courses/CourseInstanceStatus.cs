namespace RADON.Database.Models.Courses;

public class CourseInstanceStatus
{
    public string CourseInstanceStatusCode { get; set; } = null!;
    public string Name { get; set; } = null!;


    public virtual ICollection<CourseInstance> CourseInstances { get; set; } = [];
}