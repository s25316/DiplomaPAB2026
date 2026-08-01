namespace Diploma.Database.Models.Projects.ProjectEvents.Audits;

public class ProjectData
{
    public Guid ProjectDataId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;


    public Guid ProjectEventId { get; set; }
    public virtual ProjectEvent ProjectEvent { get; set; } = null!;
}