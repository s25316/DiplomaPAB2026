using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Response = RADON.Contracts.Descriptions.Courses.Response;

namespace RADON.Courses.Responses;

/// <include file='Response.xml' path='docs/members/member[@name="course_pka_data"]/summary' />
[Display(Name = nameof(Response.course_pka_data), ResourceType = typeof(Response))]
public sealed class EvaluationPkaData
{
    /// <include file='Response.xml' path='docs/members/member[@name="course_pka_rating"]/summary' />
    [Display(Name = nameof(Response.course_pka_rating), ResourceType = typeof(Response))]
    [JsonPropertyName("rating")]
    public required string? Rating { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_pka_resolution_date"]/summary' />
    [Display(Name = nameof(Response.course_pka_resolution_date), ResourceType = typeof(Response))]
    [JsonPropertyName("resolutionDate")]
    public required DateOnly ResolutionDate { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_pka_resolution_number"]/summary' />
    [Display(Name = nameof(Response.course_pka_resolution_number), ResourceType = typeof(Response))]
    [JsonPropertyName("resolutionNumber")]
    public required string? ResolutionNumber { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_pka_next_rating_year"]/summary' />
    [Display(Name = nameof(Response.course_pka_next_rating_year), ResourceType = typeof(Response))]
    [JsonPropertyName("nextRatingYear")]
    public required string? NextRatingYear { get; init; } = null;
}