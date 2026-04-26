using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RADON.Institutions.Responses;

/// <include file='Response.xml' path='docs/members/member[@name="institution_supervising_institution_data"]/summary' />
[Display(Name = nameof(Response.institution_supervising_institution_data), ResourceType = typeof(Response))]
public class SupervisingInstitutionData
{
    /// <include file='Response.xml' path='docs/members/member[@name="institution_supervising_institution_id"]/summary' />
    [Display(Name = nameof(Response.institution_supervising_institution_id), ResourceType = typeof(Response))]
    [JsonPropertyName("supervisingInstitutionID")]
    public string? SupervisingInstitutionId { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_supervising_institution_name"]/summary' />
    [Display(Name = nameof(Response.institution_supervising_institution_name), ResourceType = typeof(Response))]
    [JsonPropertyName("supervisingInstitutionName")]
    public string? SupervisingInstitutionName { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_supervising_institution_date_from"]/summary' />
    [Display(Name = nameof(Response.institution_supervising_institution_date_from), ResourceType = typeof(Response))]
    [JsonPropertyName("dateFrom")]
    public string? DateFrom { get; init; } = null;
}