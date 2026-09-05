using Diploma.Models.Dictionaries;

namespace Frontend.Components.ProjectsPublic.Layout;

public class ProjectLocalDto
{
    public required Guid ProjectId { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
    public required bool IsVisible { get; init; }
    public required bool IsAvailableRecruitment { get; init; }
    public required bool? IsRecruted { get; set; }

    public required IList<DictionaryItem<string>> Disciplines { get; init; } = [];
    public required IList<Guid> EductionInstitutionIds { get; init; } = [];
}
