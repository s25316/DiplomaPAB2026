using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Response = RADON.Contracts.Descriptions.Institutions.Response;

namespace RADON.Contracts.Institutions.Responses;

/// <include file='Response.xml' path='docs/members/member[@name="institution_supervising_institution_data"]/summary' />
[Display(Name = nameof(Response.institution_supervising_institution_data), ResourceType = typeof(Response))]
public sealed class SupervisingInstitutionData
{
    /// <include file='Response.xml' path='docs/members/member[@name="institution_supervising_institution_id"]/summary' />
    [Display(Name = nameof(Response.institution_supervising_institution_id), ResourceType = typeof(Response))]
    [JsonPropertyName("supervisingInstitutionID")]
    public required Guid SupervisingInstitutionId { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="institution_supervising_institution_name"]/summary' />
    [Display(Name = nameof(Response.institution_supervising_institution_name), ResourceType = typeof(Response))]
    [JsonPropertyName("supervisingInstitutionName")]
    public required string SupervisingInstitutionName { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="institution_supervising_institution_date_from"]/summary' />
    [Display(Name = nameof(Response.institution_supervising_institution_date_from), ResourceType = typeof(Response))]
    [JsonPropertyName("dateFrom")]
    public required DateOnly DateFrom { get; init; }
}