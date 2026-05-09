namespace RADON.Models.Courses;

public sealed class QueryParameters
{
    public string? Name { get; init; } = null;
    public ICollection<Guid> CourseUuids { get; set; } = [];
    public ICollection<Guid> InstitutionUuids { get; set; } = [];

    public bool? TeacherTraining { get; init; } = null;
    public bool? Philological { get; init; } = null;

    public bool? Dual { get; init; } = null;
    public bool? Bridging { get; init; } = null;
    public bool? CoopWithVocational { get; init; } = null;

    public ICollection<string> FormCodes { get; init; } = [];
    public ICollection<string> LevelCodes { get; init; } = [];
    public ICollection<string> ProfileCodes { get; init; } = [];
    public ICollection<string> StatusCodes { get; init; } = [];
    public ICollection<string> IscedCodes { get; init; } = [];
    public ICollection<string> LanguageCodes { get; init; } = [];
    public ICollection<string> PhilologicalLanguageCodes { get; init; } = [];
    public ICollection<string> ProfessionalTitleCodes { get; init; } = [];
    public ICollection<string> DisciplineCodes { get; init; } = [];
}