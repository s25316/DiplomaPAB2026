using RADON.Models.Dictionaries.Responses;

namespace RADON.Models.Courses.Responses;

public sealed class CourseInstance
{
    public required Guid CourseInstanceUuid { get; init; }
    public required string Name { get; init; } = null!;

    public required DateOnly EducationStartDate { get; init; }
    public required DateOnly? LiquidationDate { get; init; } = null;

    public required int NumberOfSemesters { get; init; }
    public required int Ects { get; init; }

    public required bool IsDual { get; init; }
    public required bool IsBridging { get; init; }
    public required bool IsCoopWithVocational { get; init; }

    public required DictionaryItem Form { get; init; } = null!;
    public required DictionaryItem ProfessionalTitle { get; init; } = null!;
    public required DictionaryItem Language { get; init; } = null!;
    public required DictionaryItem Status { get; init; } = null!;

    public required ICollection<DictionaryItem> PhilologicalLanguages { get; init; } = [];
}