// Ignore Spelling: ects
using RADON.Contracts.JsonConfiguration.JsonConverters;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Response = RADON.Contracts.Descriptions.Courses.Response;

namespace RADON.Courses.Responses;

/// <include file='Response.xml' path='docs/members/member[@name="course_instance_data"]/summary' />
[Display(Name = nameof(Response.course_instance_data), ResourceType = typeof(Response))]
public sealed class CourseInstanceData
{
    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_course_instance_uuid"]/summary' />
    [Display(Name = nameof(Response.course_instance_course_instance_uuid), ResourceType = typeof(Response))]
    [JsonPropertyName("courseInstanceUuid")]
    public required Guid CourseInstanceUuid { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_course_instance_code"]/summary' />
    [Display(Name = nameof(Response.course_instance_course_instance_code), ResourceType = typeof(Response))]
    [JsonPropertyName("courseInstanceCode")]
    public required int CourseInstanceCode { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_course_instance_old_code"]/summary' />
    [Display(Name = nameof(Response.course_instance_course_instance_old_code), ResourceType = typeof(Response))]
    [JsonPropertyName("courseInstanceOldCode")]
    public required string? CourseInstanceOldCode { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_course_name"]/summary' />
    [Display(Name = nameof(Response.course_instance_course_name), ResourceType = typeof(Response))]
    [JsonPropertyName("courseName")]
    public required string CourseName { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_form_code"]/summary' />
    [Display(Name = nameof(Response.course_instance_form_code), ResourceType = typeof(Response))]
    [JsonPropertyName("formCode")]
    public required string FormCode { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_form_name"]/summary' />
    [Display(Name = nameof(Response.course_instance_form_name), ResourceType = typeof(Response))]
    [JsonPropertyName("formName")]
    public required string FormName { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_title_code"]/summary' />
    [Display(Name = nameof(Response.course_instance_title_code), ResourceType = typeof(Response))]
    [JsonPropertyName("titleCode")]
    public required string TitleCode { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_title_name"]/summary' />
    [Display(Name = nameof(Response.course_instance_title_name), ResourceType = typeof(Response))]
    [JsonPropertyName("titleName")]
    public required string TitleName { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_language_code"]/summary' />
    [Display(Name = nameof(Response.course_instance_language_code), ResourceType = typeof(Response))]
    [JsonPropertyName("languageCode")]
    public required string LanguageCode { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_language_name"]/summary' />
    [Display(Name = nameof(Response.course_instance_language_name), ResourceType = typeof(Response))]
    [JsonPropertyName("languageName")]
    public required string LanguageName { get; init; }


    [JsonPropertyName("philologicalLanguages")]
    public List<LanguageData> PhilologicalLanguages { get; init; } = [];


    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_education_start_date"]/summary' />
    [Display(Name = nameof(Response.course_instance_education_start_date), ResourceType = typeof(Response))]
    [JsonPropertyName("educationStartDate")]
    public required DateOnly EducationStartDate { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_number_of_semesters"]/summary' />
    [Display(Name = nameof(Response.course_instance_number_of_semesters), ResourceType = typeof(Response))]
    [JsonPropertyName("numberOfSemesters")]
    public required int NumberOfSemesters { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_ects"]/summary' />
    [Display(Name = nameof(Response.course_instance_ects), ResourceType = typeof(Response))]
    [JsonPropertyName("ects")]
    public required int Ects { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_dual"]/summary' />
    [Display(Name = nameof(Response.course_instance_dual), ResourceType = typeof(Response))]
    [JsonPropertyName("dual")]
    [JsonConverter(typeof(PolishBoolConverter))]
    public required bool Dual { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_bridging"]/summary' />
    [Display(Name = nameof(Response.course_instance_bridging), ResourceType = typeof(Response))]
    [JsonPropertyName("bridging")]
    [JsonConverter(typeof(PolishBoolConverter))]
    public required bool Bridging { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_status_code"]/summary' />
    [Display(Name = nameof(Response.course_instance_status_code), ResourceType = typeof(Response))]
    [JsonPropertyName("statusCode")]
    public required string StatusCode { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_status_name"]/summary' />
    [Display(Name = nameof(Response.course_instance_status_name), ResourceType = typeof(Response))]
    [JsonPropertyName("statusName")]
    public required string StatusName { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_liquidation_date"]/summary' />
    [Display(Name = nameof(Response.course_instance_liquidation_date), ResourceType = typeof(Response))]
    [JsonPropertyName("liquidationDate")]
    public required DateOnly? LiquidationDate { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_coop_with_vocational"]/summary' />
    [Display(Name = nameof(Response.course_instance_coop_with_vocational), ResourceType = typeof(Response))]
    [JsonPropertyName("coopWithVocational")]
    [JsonConverter(typeof(PolishBoolConverter))]
    public required bool CoopWithVocational { get; init; }
}