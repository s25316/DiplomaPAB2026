using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Response = RADON.Contracts.Descriptions.Dictionaries.Response;

namespace RADON.Contracts.Dictionaries.Responses;

/// <include file='Response.xml' path='docs/members/member[@name="dictionary_value"]/summary' />
[Display(Name = nameof(Response.dictionary_value), ResourceType = typeof(Response))]
public sealed class DictValue
{
    /// <include file='Response.xml' path='docs/members/member[@name="dictionary_code"]/summary' />
    [Display(Name = nameof(Response.dictionary_code), ResourceType = typeof(Response))]
    [JsonPropertyName("code")]
    public required string Code { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="dictionary_name_pl"]/summary' />
    [Display(Name = nameof(Response.dictionary_name_pl), ResourceType = typeof(Response))]
    [JsonPropertyName("namePl")]
    public required string NamePl { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="dictionary_name_en"]/summary' />
    [Display(Name = nameof(Response.dictionary_name_en), ResourceType = typeof(Response))]
    [JsonPropertyName("nameEn")]
    public required string NameEn { get; init; }
}