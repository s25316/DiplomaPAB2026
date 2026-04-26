using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RADON.Institutions.Responses;

/// <include file='Response.xml' path='docs/members/member[@name="institution_target_institution_data"]/summary' />
[Display(Name = nameof(Response.institution_target_institution_data), ResourceType = typeof(Response))]
public class TargetInstitutionData : BaseInstitutionData
{
    /// <include file='Response.xml' path='docs/members/member[@name="institution_target_institution_target_institution_uuid"]/summary' />
    [Display(Name = nameof(Response.institution_target_institution_target_institution_uuid), ResourceType = typeof(Response))]
    [JsonPropertyName("targetInstitutionUuid")]
    public string? TargetInstitutionUuid { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_target_institution_target_institution_name"]/summary' />
    [Display(Name = nameof(Response.institution_target_institution_target_institution_name), ResourceType = typeof(Response))]
    [JsonPropertyName("targetInstitutionName")]
    public string? TargetInstitutionName { get; init; } = null;
}