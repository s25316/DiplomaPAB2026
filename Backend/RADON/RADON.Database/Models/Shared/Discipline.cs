namespace RADON.Database.Models.Shared;

public class Discipline
{
    public string DisciplineCode { get; set; } = null!;
    public string Name { get; set; } = null!;


    public virtual ICollection<CourseDiscipline> Courses { get; set; } = [];
}