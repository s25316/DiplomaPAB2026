using RADON.Models.Responses.Dictionaries;

namespace RADON.Models.Responses.Courses;

public sealed class CourseInstance
{
    public Guid CourseInstanceUuid { get; init; }
    public string Name { get; init; } = null!;

    public DateOnly EducationStartDate { get; init; }
    public DateOnly? LiquidationDate { get; init; } = null;

    public int NumberOfSemesters { get; init; }
    public int Ects { get; init; }

    public bool Dual { get; init; }
    public bool Bridging { get; init; }
    public bool CoopWithVocational { get; init; }

    public DictionaryItem CourseForm { get; init; } = null!;
    public DictionaryItem ProfessionalTitle { get; init; } = null!;
    public DictionaryItem Language { get; init; } = null!;
    public DictionaryItem CourseInstanceStatus { get; init; } = null!;

    public ICollection<DictionaryItem> PhilologicalLanguages { get; init; } = [];
}