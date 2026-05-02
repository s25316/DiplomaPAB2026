using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Response = RADON.Contracts.Descriptions.Courses.Response;

namespace RADON.Courses.Responses;

/// <include file='Response.xml' path='docs/members/member[@name="course_organizational_unit_data"]/summary' />
[Display(Name = nameof(Response.course_organizational_unit_data), ResourceType = typeof(Response))]
public class OrganizationalUnitData
{
    /// <include file='Response.xml' path='docs/members/member[@name="course_organizational_unit_organizational_unit_uuid"]/summary' />
    [Display(Name = nameof(Response.course_organizational_unit_organizational_unit_uuid), ResourceType = typeof(Response))]
    [JsonPropertyName("organizationalUnitUuid")]
    public string? OrganizationalUnitUuid { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_organizational_unit_organizational_unit_full_name"]/summary' />
    [Display(Name = nameof(Response.course_organizational_unit_organizational_unit_full_name), ResourceType = typeof(Response))]
    [JsonPropertyName("organizationalUnitFullName")]
    public string? OrganizationalUnitFullName { get; init; } = null;
}