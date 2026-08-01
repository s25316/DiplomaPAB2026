using Diploma.Database.Models.Projects.ProjectEvents;

namespace Diploma.Database.Models.Projects;

public class Project
{
    public Guid ProjectId { get; set; }
    public DateTimeOffset CreateAt { get; set; }
    public DateTimeOffset? RemovedAt { get; set; }


    public virtual ICollection<ProjectEvent> ProjectEvents { get; set; } = [];
}