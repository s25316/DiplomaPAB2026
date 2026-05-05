namespace RADON.Database.Models.Courses;

public class CourseStatus
{
    public string CourseStatusCode { get; set; } = null!;
    public string Name { get; set; } = null!;


    public virtual ICollection<Course> Courses { get; set; } = [];
}