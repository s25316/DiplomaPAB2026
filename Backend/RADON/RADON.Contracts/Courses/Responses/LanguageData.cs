using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Response = RADON.Contracts.Descriptions.Courses.Response;

namespace RADON.Courses.Responses;

/// <include file='Response.xml' path='docs/members/member[@name="course_language_data"]/summary' />
[Display(Name = nameof(Response.course_language_data), ResourceType = typeof(Response))]
public sealed class LanguageData
{
    /// <include file='Response.xml' path='docs/members/member[@name="course_language_language_code"]/summary' />
    [Display(Name = nameof(Response.course_language_language_code), ResourceType = typeof(Response))]
    [JsonPropertyName("languageCode")]
    public required string LanguageCode { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_language_language_name"]/summary' />
    [Display(Name = nameof(Response.course_language_language_name), ResourceType = typeof(Response))]
    [JsonPropertyName("languageName")]
    public required string LanguageName { get; init; }
}