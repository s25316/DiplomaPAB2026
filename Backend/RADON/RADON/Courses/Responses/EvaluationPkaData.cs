using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RADON.Courses.Responses;

/// <include file='Response.xml' path='docs/members/member[@name="course_pka_data"]/summary' />
[Display(Name = nameof(Response.course_pka_data), ResourceType = typeof(Response))]
public class EvaluationPkaData
{
    /// <include file='Response.xml' path='docs/members/member[@name="course_pka_rating"]/summary' />
    [Display(Name = nameof(Response.course_pka_rating), ResourceType = typeof(Response))]
    [JsonPropertyName("rating")]
    public string? Rating { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_pka_resolution_date"]/summary' />
    [Display(Name = nameof(Response.course_pka_resolution_date), ResourceType = typeof(Response))]
    [JsonPropertyName("resolutionDate")]
    public string? ResolutionDate { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_pka_resolution_number"]/summary' />
    [Display(Name = nameof(Response.course_pka_resolution_number), ResourceType = typeof(Response))]
    [JsonPropertyName("resolutionNumber")]
    public string? ResolutionNumber { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_pka_next_rating_year"]/summary' />
    [Display(Name = nameof(Response.course_pka_next_rating_year), ResourceType = typeof(Response))]
    [JsonPropertyName("nextRatingYear")]
    public string? NextRatingYear { get; init; } = null;
}