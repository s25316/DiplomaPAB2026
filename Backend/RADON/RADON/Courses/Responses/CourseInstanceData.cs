using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RADON.Courses.Responses;

/// <include file='Response.xml' path='docs/members/member[@name="course_instance_data"]/summary' />
[Display(Name = nameof(Response.course_instance_data), ResourceType = typeof(Response))]
public class CourseInstanceData
{
    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_course_instance_uuid"]/summary' />
    [Display(Name = nameof(Response.course_instance_course_instance_uuid), ResourceType = typeof(Response))]
    [JsonPropertyName("courseInstanceUuid")]
    public string? CourseInstanceUuid { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_course_instance_code"]/summary' />
    [Display(Name = nameof(Response.course_instance_course_instance_code), ResourceType = typeof(Response))]
    [JsonPropertyName("courseInstanceCode")]
    public string? CourseInstanceCode { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_course_instance_old_code"]/summary' />
    [Display(Name = nameof(Response.course_instance_course_instance_old_code), ResourceType = typeof(Response))]
    [JsonPropertyName("courseInstanceOldCode")]
    public string? CourseInstanceOldCode { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_course_name"]/summary' />
    [Display(Name = nameof(Response.course_instance_course_name), ResourceType = typeof(Response))]
    [JsonPropertyName("courseName")]
    public string? CourseName { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_form_code"]/summary' />
    [Display(Name = nameof(Response.course_instance_form_code), ResourceType = typeof(Response))]
    [JsonPropertyName("formCode")]
    public string? FormCode { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_form_name"]/summary' />
    [Display(Name = nameof(Response.course_instance_form_name), ResourceType = typeof(Response))]
    [JsonPropertyName("formName")]
    public string? FormName { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_title_code"]/summary' />
    [Display(Name = nameof(Response.course_instance_title_code), ResourceType = typeof(Response))]
    [JsonPropertyName("titleCode")]
    public string? TitleCode { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_title_name"]/summary' />
    [Display(Name = nameof(Response.course_instance_title_name), ResourceType = typeof(Response))]
    [JsonPropertyName("titleName")]
    public string? TitleName { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_language_code"]/summary' />
    [Display(Name = nameof(Response.course_instance_language_code), ResourceType = typeof(Response))]
    [JsonPropertyName("languageCode")]
    public string? LanguageCode { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_language_name"]/summary' />
    [Display(Name = nameof(Response.course_instance_language_name), ResourceType = typeof(Response))]
    [JsonPropertyName("languageName")]
    public string? LanguageName { get; init; } = null;


    [JsonPropertyName("philologicalLanguages")]
    public List<LanguageData> PhilologicalLanguages { get; init; } = [];


    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_education_start_date"]/summary' />
    [Display(Name = nameof(Response.course_instance_education_start_date), ResourceType = typeof(Response))]
    [JsonPropertyName("educationStartDate")]
    public string? EducationStartDate { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_number_of_semesters"]/summary' />
    [Display(Name = nameof(Response.course_instance_number_of_semesters), ResourceType = typeof(Response))]
    [JsonPropertyName("numberOfSemesters")]
    public string? NumberOfSemesters { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_ects"]/summary' />
    [Display(Name = nameof(Response.course_instance_ects), ResourceType = typeof(Response))]
    [JsonPropertyName("ects")]
    public string? Ects { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_dual"]/summary' />
    [Display(Name = nameof(Response.course_instance_dual), ResourceType = typeof(Response))]
    [JsonPropertyName("dual")]
    public string? Dual { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_bridging"]/summary' />
    [Display(Name = nameof(Response.course_instance_bridging), ResourceType = typeof(Response))]
    [JsonPropertyName("bridging")]
    public string? Bridging { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_status_code"]/summary' />
    [Display(Name = nameof(Response.course_instance_status_code), ResourceType = typeof(Response))]
    [JsonPropertyName("statusCode")]
    public string? StatusCode { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_status_name"]/summary' />
    [Display(Name = nameof(Response.course_instance_status_name), ResourceType = typeof(Response))]
    [JsonPropertyName("statusName")]
    public string? StatusName { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_liquidation_date"]/summary' />
    [Display(Name = nameof(Response.course_instance_liquidation_date), ResourceType = typeof(Response))]
    [JsonPropertyName("liquidationDate")]
    public string? LiquidationDate { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_instance_coop_with_vocational"]/summary' />
    [Display(Name = nameof(Response.course_instance_coop_with_vocational), ResourceType = typeof(Response))]
    [JsonPropertyName("coopWithVocational")]
    public string? CoopWithVocational { get; init; } = null;
}