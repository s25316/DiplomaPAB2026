using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Response = RADON.Contracts.Descriptions.Courses.Response;

namespace RADON.Courses.Responses;

/// <include file='Response.xml' path='docs/members/member[@name="course_organizational_unit_data"]/summary' />
[Display(Name = nameof(Response.course_organizational_unit_data), ResourceType = typeof(Response))]
public sealed class OrganizationalUnitData
{
    /// <include file='Response.xml' path='docs/members/member[@name="course_organizational_unit_organizational_unit_uuid"]/summary' />
    [Display(Name = nameof(Response.course_organizational_unit_organizational_unit_uuid), ResourceType = typeof(Response))]
    [JsonPropertyName("organizationalUnitUuid")]
    public required Guid OrganizationalUnitUuid { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_organizational_unit_organizational_unit_full_name"]/summary' />
    [Display(Name = nameof(Response.course_organizational_unit_organizational_unit_full_name), ResourceType = typeof(Response))]
    [JsonPropertyName("organizationalUnitFullName")]
    public required string OrganizationalUnitFullName { get; init; }
}