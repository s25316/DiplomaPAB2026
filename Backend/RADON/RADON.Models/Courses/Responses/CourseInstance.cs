using RADON.Models.Dictionaries.Responses;
using System.ComponentModel.DataAnnotations;
using Response = RADON.Models.Descriptions.Courses.Response;

namespace RADON.Models.Courses.Responses;

/// <include file='Response.xml' path='docs/members/member[@name="CourseInstance"]/summary' />
[Display(Name = nameof(Response.CourseInstance), ResourceType = typeof(Response))]
public sealed class CourseInstance
{
    /// <include file='Response.xml' path='docs/members/member[@name="CourseInstance_CourseInstanceUuid"]/summary' />
    [Display(Name = nameof(Response.CourseInstance_CourseInstanceUuid), ResourceType = typeof(Response))]
    public required Guid CourseInstanceUuid { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="CourseInstance_Name"]/summary' />
    [Display(Name = nameof(Response.CourseInstance_Name), ResourceType = typeof(Response))]
    public required string Name { get; init; } = null!;


    /// <include file='Response.xml' path='docs/members/member[@name="CourseInstance_EducationStartDate"]/summary' />
    [Display(Name = nameof(Response.CourseInstance_EducationStartDate), ResourceType = typeof(Response))]
    public required DateOnly EducationStartDate { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="CourseInstance_LiquidationDate"]/summary' />
    [Display(Name = nameof(Response.CourseInstance_LiquidationDate), ResourceType = typeof(Response))]
    public required DateOnly? LiquidationDate { get; init; } = null;


    /// <include file='Response.xml' path='docs/members/member[@name="CourseInstance_NumberOfSemesters"]/summary' />
    [Display(Name = nameof(Response.CourseInstance_NumberOfSemesters), ResourceType = typeof(Response))]
    public required int NumberOfSemesters { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="CourseInstance_Ects"]/summary' />
    [Display(Name = nameof(Response.CourseInstance_Ects), ResourceType = typeof(Response))]
    public required int Ects { get; init; }


    /// <include file='Response.xml' path='docs/members/member[@name="CourseInstance_IsDual"]/summary' />
    [Display(Name = nameof(Response.CourseInstance_IsDual), ResourceType = typeof(Response))]
    public required bool IsDual { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="CourseInstance_IsBridging"]/summary' />
    [Display(Name = nameof(Response.CourseInstance_IsBridging), ResourceType = typeof(Response))]
    public required bool IsBridging { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="CourseInstance_IsCoopWithVocational"]/summary' />
    [Display(Name = nameof(Response.CourseInstance_IsCoopWithVocational), ResourceType = typeof(Response))]
    public required bool IsCoopWithVocational { get; init; }


    /// <include file='Response.xml' path='docs/members/member[@name="CourseInstance_Form"]/summary' />
    [Display(Name = nameof(Response.CourseInstance_Form), ResourceType = typeof(Response))]
    public required DictionaryItem Form { get; init; } = null!;

    /// <include file='Response.xml' path='docs/members/member[@name="CourseInstance_ProfessionalTitle"]/summary' />
    [Display(Name = nameof(Response.CourseInstance_ProfessionalTitle), ResourceType = typeof(Response))]
    public required DictionaryItem ProfessionalTitle { get; init; } = null!;

    /// <include file='Response.xml' path='docs/members/member[@name="CourseInstance_Language"]/summary' />
    [Display(Name = nameof(Response.CourseInstance_Language), ResourceType = typeof(Response))]
    public required DictionaryItem Language { get; init; } = null!;

    /// <include file='Response.xml' path='docs/members/member[@name="CourseInstance_Status"]/summary' />
    [Display(Name = nameof(Response.CourseInstance_Status), ResourceType = typeof(Response))]
    public required DictionaryItem Status { get; init; } = null!;


    /// <include file='Response.xml' path='docs/members/member[@name="CourseInstance_PhilologicalLanguages"]/summary' />
    [Display(Name = nameof(Response.CourseInstance_PhilologicalLanguages), ResourceType = typeof(Response))]
    public required ICollection<DictionaryItem> PhilologicalLanguages { get; init; } = [];
}