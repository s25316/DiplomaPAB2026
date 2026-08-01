namespace Diploma.Database.Models.Projects.ProjectEvents.Audits.ProjectVisibilities;

public class ProjectVisibilityType
{
    public int ProjectVisibilityTypeId { get; set; }
    public string Name { get; set; } = null!;


    public virtual ICollection<ProjectVisibility> ProjectVisibilities { get; set; } = [];
}