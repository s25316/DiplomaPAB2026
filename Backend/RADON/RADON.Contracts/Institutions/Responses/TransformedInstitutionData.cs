using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Response = RADON.Contracts.Descriptions.Institutions.Response;

namespace RADON.Contracts.Institutions.Responses;
/// <include file='Response.xml' path='docs/members/member[@name="institution_transformed_institution_data"]/summary' />
[Display(Name = nameof(Response.institution_transformed_institution_data), ResourceType = typeof(Response))]
public class TransformedInstitutionData : BaseInstitutionData
{
    /// <include file='Response.xml' path='docs/members/member[@name="institution_transformed_institution_transformed_institution_uuid"]/summary' />
    [Display(Name = nameof(Response.institution_transformed_institution_transformed_institution_uuid), ResourceType = typeof(Response))]
    [JsonPropertyName("transformedInstitutionUuid")]
    public required Guid TransformedInstitutionUuid { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="institution_transformed_institution_transformed_institution_name"]/summary' />
    [Display(Name = nameof(Response.institution_transformed_institution_transformed_institution_name), ResourceType = typeof(Response))]
    [JsonPropertyName("transformedInstitutionName")]
    public required string TransformedInstitutionName { get; init; }
}