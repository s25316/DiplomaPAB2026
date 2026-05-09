using RADON.Database.Models.Courses;

namespace RADON.Database.Models.Shared;

public class CourseDiscipline
{
    public Guid CourseDisciplineUuid { get; set; }
    public int PercentageShare { get; set; }
    public bool Leading { get; set; }


    public Guid CourseUuid { get; set; }
    public virtual Course Course { get; set; } = null!;

    public string DisciplineCode { get; set; } = null!;
    public virtual Discipline Discipline { get; set; } = null!;
}