using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Response = RADON.Contracts.Descriptions.Shared.Response;

namespace RADON.Contracts.Shared.Responses;

public class Pagination
{
    /// <include file='Response.xml' path='docs/members/member[@name="max_count"]/summary' />
    [DefaultValue(1)]
    [Display(Name = nameof(Response.max_count), ResourceType = typeof(Response))]
    [JsonPropertyName("maxCount")]
    public required int MaxCount { get; init; } = 1;

    /// <include file='Response.xml' path='docs/members/member[@name="token"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(Response.token), ResourceType = typeof(Response))]
    [JsonPropertyName("token")]
    public string? Token { get; init; } = null;
}