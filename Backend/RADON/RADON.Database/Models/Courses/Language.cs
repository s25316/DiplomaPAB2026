namespace RADON.Database.Models.Courses;

public class Language
{
    public string LanguageCode { get; set; } = null!;
    public string Name { get; set; } = null!;


    public virtual ICollection<CourseInstance> CourseInstances { get; set; } = [];
    public virtual ICollection<CourseInstance> CourseInstancesPhilological { get; set; } = [];
}