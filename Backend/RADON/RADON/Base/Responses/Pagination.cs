using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ResponseFile = RADON.Response;

namespace RADON.Base.Responses;

public class Pagination
{
    /// <include file='Response.xml' path='docs/members/member[@name="max_count"]/summary' />
    [DefaultValue(1)]
    [Display(Name = nameof(ResponseFile.max_count), ResourceType = typeof(ResponseFile))]
    [JsonPropertyName("maxCount")]
    public required int MaxCount { get; init; } = 1;

    /// <include file='Response.xml' path='docs/members/member[@name="token"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(ResponseFile.token), ResourceType = typeof(ResponseFile))]
    [JsonPropertyName("token")]
    public string? Token { get; init; } = null;
}