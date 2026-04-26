using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RADON.Institutions.Responses;

/// <include file='Response.xml' path='docs/members/member[@name="institution_federation_institution_data"]/summary' />
[Display(Name = nameof(Response.institution_federation_institution_data), ResourceType = typeof(Response))]
public class FederationInstitutionData
{
    /// <include file='Response.xml' path='docs/members/member[@name="institution_federation_institution_institution_uuid"]/summary' />
    [Display(Name = nameof(Response.institution_federation_institution_institution_uuid), ResourceType = typeof(Response))]
    [JsonPropertyName("institutionUuid")]
    public required Guid InstitutionUuid { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="institution_federation_institution_institution_name"]/summary' />
    [Display(Name = nameof(Response.institution_federation_institution_institution_name), ResourceType = typeof(Response))]
    [JsonPropertyName("institutionName")]
    public required string InstitutionName { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="institution_federation_institution_date_from"]/summary' />
    [Display(Name = nameof(Response.institution_federation_institution_date_from), ResourceType = typeof(Response))]
    [JsonPropertyName("dateFrom")]
    public required string DateFrom { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="institution_federation_institution_date_to"]/summary' />
    [Display(Name = nameof(Response.institution_federation_institution_date_to), ResourceType = typeof(Response))]
    [JsonPropertyName("dateTo")]
    public string? DateTo { get; init; } = null;
}