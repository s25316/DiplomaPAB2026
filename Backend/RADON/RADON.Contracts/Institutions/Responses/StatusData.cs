using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Response = RADON.Contracts.Descriptions.Institutions.Response;

namespace RADON.Contracts.Institutions.Responses;

/// <include file='Response.xml' path='docs/members/member[@name="institution_status_data"]/summary' />
[Display(Name = nameof(Response.institution_status_data), ResourceType = typeof(Response))]
public class StatusData
{
    /// <include file='Response.xml' path='docs/members/member[@name="institution_status_name"]/summary' />
    [Display(Name = nameof(Response.institution_status_name), ResourceType = typeof(Response))]
    [JsonPropertyName("statusName")]
    public required string StatusName { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="institution_status_date_from"]/summary' />
    [Display(Name = nameof(Response.institution_status_date_from), ResourceType = typeof(Response))]
    [JsonPropertyName("dateFrom")]
    public required string DateFrom { get; init; }
}