using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Response = RADON.Contracts.Descriptions.Courses.Response;

namespace RADON.Courses.Responses;

/// <include file='Response.xml' path='docs/members/member[@name="course_co_leading_institution_data"]/summary' />
[Display(Name = nameof(Response.course_co_leading_institution_data), ResourceType = typeof(Response))]
public class CoLeadingInstitutionData
{
    /// <include file='Response.xml' path='docs/members/member[@name="course_co_leading_institution_co_leading_institution_uuid"]/summary' />
    [Display(Name = nameof(Response.course_co_leading_institution_co_leading_institution_uuid), ResourceType = typeof(Response))]
    [JsonPropertyName("coLeadingInstitutionUuid")]
    public string? CoLeadingInstitutionUuid { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_co_leading_institution_co_leading_institution_name"]/summary' />
    [Display(Name = nameof(Response.course_co_leading_institution_co_leading_institution_name), ResourceType = typeof(Response))]
    [JsonPropertyName("coLeadingInstitutionName")]
    public string? CoLeadingInstitutionName { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_co_leading_institution_is_foreign"]/summary' />
    [Display(Name = nameof(Response.course_co_leading_institution_is_foreign), ResourceType = typeof(Response))]
    [JsonPropertyName("isForeign")]
    public string? IsForeign { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_co_leading_institution_course_uuid"]/summary' />
    [Display(Name = nameof(Response.course_co_leading_institution_course_uuid), ResourceType = typeof(Response))]
    [JsonPropertyName("courseUuid")]
    public string? CourseUuid { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_co_leading_institution_course_name"]/summary' />
    [Display(Name = nameof(Response.course_co_leading_institution_course_name), ResourceType = typeof(Response))]
    [JsonPropertyName("courseName")]
    public string? CourseName { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_co_leading_institution_date_from"]/summary' />
    [Display(Name = nameof(Response.course_co_leading_institution_date_from), ResourceType = typeof(Response))]
    [JsonPropertyName("dateFrom")]
    public string? DateFrom { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_co_leading_institution_date_to"]/summary' />
    [Display(Name = nameof(Response.course_co_leading_institution_date_to), ResourceType = typeof(Response))]
    [JsonPropertyName("dateTo")]
    public string? DateTo { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_co_leading_institution_co_led_fos_confirmation_status"]/summary' />
    [Display(Name = nameof(Response.course_co_leading_institution_co_led_fos_confirmation_status), ResourceType = typeof(Response))]
    [JsonPropertyName("coLedFosConfirmationStatus")]
    public string? CoLedFosConfirmationStatus { get; init; } = null;
}