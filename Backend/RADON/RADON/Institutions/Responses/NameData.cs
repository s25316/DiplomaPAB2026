using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RADON.Institutions.Responses;

/// <include file='Response.xml' path='docs/members/member[@name="institution_name_data"]/summary' />
[Display(Name = nameof(Response.institution_name_data), ResourceType = typeof(Response))]
public class NameData
{
    /// <include file='Response.xml' path='docs/members/member[@name="institution_name_data_name"]/summary' />
    [Display(Name = nameof(Response.institution_name_data_name), ResourceType = typeof(Response))]
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="institution_name_data_date_from"]/summary' />
    [Display(Name = nameof(Response.institution_name_data_date_from), ResourceType = typeof(Response))]
    [JsonPropertyName("dateFrom")]
    public required string DateFrom { get; init; }
}