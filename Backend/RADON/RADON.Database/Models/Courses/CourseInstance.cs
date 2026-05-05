namespace RADON.Database.Models.Courses;

public class CourseInstance
{
    public Guid CourseInstanceUuid { get; set; }
    public string Name { get; set; } = null!;

    public DateOnly EducationStartDate { get; set; }
    public DateOnly? LiquidationDate { get; set; } = null;

    public int NumberOfSemesters { get; set; }
    public int Ects { get; set; }

    public bool Dual { get; set; }
    public bool Bridging { get; set; }
    public bool CoopWithVocational { get; set; }


    public Guid CourseUuid { get; init; }
    public virtual Course Course { get; set; } = null!;

    public string CourseFormCode { get; set; } = null!;
    public virtual CourseForm CourseForm { get; set; } = null!;

    public string ProfessionalTitleCode { get; set; } = null!;
    public virtual ProfessionalTitle ProfessionalTitle { get; set; } = null!;

    public string LanguageCode { get; set; } = null!;
    public virtual Language Language { get; set; } = null!;

    public string CourseInstanceStatusCode { get; set; } = null!;
    public virtual CourseInstanceStatus CourseInstanceStatus { get; set; } = null!;

    public virtual ICollection<Language> PhilologicalLanguages { get; set; } = [];
}