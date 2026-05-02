using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Response = RADON.Contracts.Descriptions.Institutions.Response;

namespace RADON.Contracts.Institutions.Responses;

/// <include file='Response.xml' path='docs/members/member[@name="institution_type_data"]/summary' />
[Display(Name = nameof(Response.institution_type_data), ResourceType = typeof(Response))]
public sealed class TypeData
{
    /// <include file='Response.xml' path='docs/members/member[@name="institution_type_name"]/summary' />
    [Display(Name = nameof(Response.institution_type_name), ResourceType = typeof(Response))]
    [JsonPropertyName("typeName")]
    public required string TypeName { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="institution_type_date_from"]/summary' />
    [Display(Name = nameof(Response.institution_type_date_from), ResourceType = typeof(Response))]
    [JsonPropertyName("dateFrom")]
    public required DateOnly DateFrom { get; init; }
}