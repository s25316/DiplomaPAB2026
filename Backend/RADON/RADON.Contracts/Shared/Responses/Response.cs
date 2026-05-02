using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Response = RADON.Contracts.Descriptions.Shared.Response;

namespace RADON.Contracts.Shared.Responses;

public sealed class Response<TResponseItem>
    where TResponseItem : class
{
    [JsonPropertyName("results")]
    public required List<TResponseItem> Results { get; init; } = [];

    [JsonPropertyName("pagination")]
    public required Pagination Pagination { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="version"]/summary' />
    [Display(Name = nameof(Response.version), ResourceType = typeof(Response))]
    [JsonPropertyName("version")]
    public required string Version { get; init; }
}