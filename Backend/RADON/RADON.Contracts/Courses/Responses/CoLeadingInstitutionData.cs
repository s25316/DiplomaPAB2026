using RADON.Contracts.JsonConfiguration.JsonConverters;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Response = RADON.Contracts.Descriptions.Courses.Response;

namespace RADON.Courses.Responses;

/// <include file='Response.xml' path='docs/members/member[@name="course_co_leading_institution_data"]/summary' />
[Display(Name = nameof(Response.course_co_leading_institution_data), ResourceType = typeof(Response))]
public sealed class CoLeadingInstitutionData
{
    /// <include file='Response.xml' path='docs/members/member[@name="course_co_leading_institution_co_leading_institution_uuid"]/summary' />
    [Display(Name = nameof(Response.course_co_leading_institution_co_leading_institution_uuid), ResourceType = typeof(Response))]
    [JsonPropertyName("coLeadingInstitutionUuid")]
    public required Guid? CoLeadingInstitutionUuid { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_co_leading_institution_co_leading_institution_name"]/summary' />
    [Display(Name = nameof(Response.course_co_leading_institution_co_leading_institution_name), ResourceType = typeof(Response))]
    [JsonPropertyName("coLeadingInstitutionName")]
    public required string CoLeadingInstitutionName { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_co_leading_institution_is_foreign"]/summary' />
    [Display(Name = nameof(Response.course_co_leading_institution_is_foreign), ResourceType = typeof(Response))]
    [JsonPropertyName("isForeign")]
    [JsonConverter(typeof(PolishBoolConverter))]
    public required bool IsForeign { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_co_leading_institution_course_uuid"]/summary' />
    [Display(Name = nameof(Response.course_co_leading_institution_course_uuid), ResourceType = typeof(Response))]
    [JsonPropertyName("courseUuid")]
    public required Guid? CourseUuid { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_co_leading_institution_course_name"]/summary' />
    [Display(Name = nameof(Response.course_co_leading_institution_course_name), ResourceType = typeof(Response))]
    [JsonPropertyName("courseName")]
    public required string? CourseName { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_co_leading_institution_date_from"]/summary' />
    [Display(Name = nameof(Response.course_co_leading_institution_date_from), ResourceType = typeof(Response))]
    [JsonPropertyName("dateFrom")]
    public required DateOnly DateFrom { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_co_leading_institution_date_to"]/summary' />
    [Display(Name = nameof(Response.course_co_leading_institution_date_to), ResourceType = typeof(Response))]
    [JsonPropertyName("dateTo")]
    public required DateOnly? DateTo { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_co_leading_institution_co_led_fos_confirmation_status"]/summary' />
    [Display(Name = nameof(Response.course_co_leading_institution_co_led_fos_confirmation_status), ResourceType = typeof(Response))]
    [JsonPropertyName("coLedFosConfirmationStatus")]
    public required string? CoLedFosConfirmationStatus { get; init; } = null;
}