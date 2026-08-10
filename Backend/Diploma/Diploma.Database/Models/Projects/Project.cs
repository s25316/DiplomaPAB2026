using Diploma.Database.Models.Projects.ProjectEvents;
using Diploma.Database.Models.Projects.ProjectRoles;

namespace Diploma.Database.Models.Projects;

public class Project
{
    public Guid ProjectId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? RemovedAt { get; set; }

    public Guid? LastProjectDataId { get; set; }
    public virtual ProjectData? LastProjectData { get; set; } = null;

    public virtual ICollection<ProjectRole> ProjectRoles { get; set; } = [];
    public virtual ICollection<ProjectEvent> ProjectEvents { get; set; } = [];
}