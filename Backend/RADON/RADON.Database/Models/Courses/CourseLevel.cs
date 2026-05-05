namespace RADON.Database.Models.Courses;

public class CourseLevel
{
    public string CourseLevelCode { get; set; } = null!;
    public string Name { get; set; } = null!;


    public virtual ICollection<Course> Courses { get; set; } = [];
}