using RADON.Models.Dictionaries.Responses;

namespace RADON.Models.Courses.Responses;

public sealed class Course
{
    public record DisciplineData
    {
        public required DictionaryItem Discipline { get; init; }
        public required int Percentage { get; init; }
        public required bool IsLeading { get; init; }
    };


    public required Guid CourseUuid { get; init; }
    public required string Name { get; init; }

    public required Guid InstitutionUuid { get; init; }

    public required DateOnly? CreationDate { get; init; }
    public required DateOnly? TerminationInitializationDate { get; init; } = null;
    public required DateOnly? LiquidationDate { get; init; } = null;

    public required bool IsTeacherTraining { get; init; }
    public required bool IsPhilological { get; init; }

    public required DictionaryItem Level { get; init; } = null!;
    public required DictionaryItem Profile { get; init; } = null!;
    public required DictionaryItem Isced { get; init; } = null!;
    public required DictionaryItem Status { get; init; } = null!;

    public required ICollection<DisciplineData> Disciplines { get; init; } = [];
    public required ICollection<CourseInstance> CourseInstances { get; init; } = [];

    public required DateTimeOffset LastRefresh { get; init; }
    public required DateTimeOffset SourceLastRefresh { get; init; }
    public required string DataSource { get; init; } = null!;
}