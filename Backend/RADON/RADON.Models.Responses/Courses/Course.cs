using RADON.Models.Responses.Dictionaries;

namespace RADON.Models.Responses.Courses;

public sealed class Course
{
    public record DisciplineData(DictionaryItem Discipline, int Percentage, bool IsLeading);


    public Guid CourseUuid { get; init; }
    public string Name { get; init; } = null!;

    public DateOnly? CreationDate { get; init; }
    public DateOnly? TerminationInitializationDate { get; init; } = null;
    public DateOnly? LiquidationDate { get; init; } = null;

    public bool TeacherTraining { get; init; }
    public bool Philological { get; init; }

    public Guid InstitutionUuid { get; init; }

    public DictionaryItem CourseLevel { get; init; } = null!;
    public DictionaryItem CourseProfile { get; init; } = null!;
    public DictionaryItem Isced { get; init; } = null!;
    public DictionaryItem CourseStatus { get; init; } = null!;

    public ICollection<DisciplineData> Disciplines { get; init; } = [];
    public ICollection<CourseInstance> CourseInstances { get; init; } = [];

    public DateTimeOffset LastRefresh { get; init; }
    public DateTimeOffset SourceLastRefresh { get; init; }
    public string DataSource { get; init; } = null!;
}