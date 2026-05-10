using RADON.Database.Models.Institutions;
using RADON.Database.Models.Shared;

namespace RADON.Database.Models.Courses;

public class Course
{
    public Guid CourseUuid { get; set; }
    public string Name { get; set; } = null!;

    public DateOnly? CreationDate { get; set; }
    public DateOnly? TerminationInitializationDate { get; set; } = null;
    public DateOnly? LiquidationDate { get; set; } = null;

    public bool IsTeacherTraining { get; set; }
    public bool IsPhilological { get; set; }

    public DateTimeOffset LastRefresh { get; set; }
    public DateTimeOffset SourceLastRefresh { get; set; }


    public Guid DataSourceId { get; set; }
    public virtual DataSource DataSource { get; set; } = null!;

    public string CourseLevelCode { get; set; } = null!;
    public virtual CourseLevel CourseLevel { get; set; } = null!;

    public string CourseProfileCode { get; set; } = null!;
    public virtual CourseProfile CourseProfile { get; set; } = null!;

    public string IscedCode { get; set; } = null!;
    public virtual Isced Isced { get; set; } = null!;

    public string CourseStatusCode { get; set; } = null!;
    public virtual CourseStatus CourseStatus { get; set; } = null!;

    public Guid InstitutionUuid { get; set; }
    public virtual Institution Institution { get; set; } = null!;


    public virtual ICollection<CourseDiscipline> Disciplines { get; set; } = [];
    public virtual ICollection<CourseInstance> CourseInstances { get; set; } = [];
}