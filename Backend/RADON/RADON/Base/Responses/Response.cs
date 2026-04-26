using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ResponseFile = RADON.Response;

namespace RADON.Base.Responses;

public class Response<TResponseItem>
    where TResponseItem : class
{
    [JsonPropertyName("results")]
    public required List<TResponseItem> Results { get; init; } = [];

    [JsonPropertyName("pagination")]
    public required Pagination Pagination { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="version"]/summary' />
    [Display(Name = nameof(ResponseFile.version), ResourceType = typeof(ResponseFile))]
    [JsonPropertyName("version")]
    public required string Version { get; init; }
}