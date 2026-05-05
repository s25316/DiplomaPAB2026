namespace RADON.Database.Models.Courses;

public class Isced
{
    public string IscedCode { get; set; } = null!;
    public string Name { get; set; } = null!;


    public virtual ICollection<Course> Courses { get; set; } = [];
}