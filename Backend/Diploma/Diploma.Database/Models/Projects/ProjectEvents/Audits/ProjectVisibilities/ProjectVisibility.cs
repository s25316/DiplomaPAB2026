namespace Diploma.Database.Models.Projects.ProjectEvents.Audits.ProjectVisibilities;

public class ProjectVisibility
{
    public Guid ProjectVisibilityId { get; set; }


    public int ProjectVisibilityTypeId { get; set; }
    public virtual ProjectVisibilityType ProjectVisibilityType { get; set; } = null!;

    public Guid ProjectEventId { get; set; }
    public virtual ProjectEvent ProjectEvent { get; set; } = null!;
}
