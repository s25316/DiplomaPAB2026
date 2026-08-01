namespace Diploma.Database.Models.Projects.ProjectEvents;

public class ProjectEventType
{
    public int ProjectEventTypeId { get; set; }
    public string Name { get; set; } = null!;


    public virtual ICollection<ProjectEvent> ProjectEvents { get; set; } = [];
}