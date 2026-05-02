using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Response = RADON.Contracts.Descriptions.Courses.Response;

namespace RADON.Courses.Responses;

/// <include file='Response.xml' path='docs/members/member[@name="course_discipline_data"]/summary' />
[Display(Name = nameof(Response.course_discipline_data), ResourceType = typeof(Response))]
public class DisciplineData
{
    /// <include file='Response.xml' path='docs/members/member[@name="course_discipline_discipline_code"]/summary' />
    [Display(Name = nameof(Response.course_discipline_discipline_code), ResourceType = typeof(Response))]
    [JsonPropertyName("disciplineCode")]
    public string? DisciplineCode { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_discipline_discipline_name"]/summary' />
    [Display(Name = nameof(Response.course_discipline_discipline_name), ResourceType = typeof(Response))]
    [JsonPropertyName("disciplineName")]
    public string? DisciplineName { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_discipline_discipline_percentage_share"]/summary' />
    [Display(Name = nameof(Response.course_discipline_discipline_percentage_share), ResourceType = typeof(Response))]
    [JsonPropertyName("disciplinePercentageShare")]
    public string? DisciplinePercentageShare { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_discipline_discipline_leading"]/summary' />
    [Display(Name = nameof(Response.course_discipline_discipline_leading), ResourceType = typeof(Response))]
    [JsonPropertyName("disciplineLeading")]
    public string? DisciplineLeading { get; init; } = null;
}