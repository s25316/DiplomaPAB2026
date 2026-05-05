namespace RADON.Database.Models.Courses;

public class CourseProfile
{
    public string CourseProfileCode { get; set; } = null!;
    public string Name { get; set; } = null!;


    public virtual ICollection<Course> Courses { get; set; } = [];
}