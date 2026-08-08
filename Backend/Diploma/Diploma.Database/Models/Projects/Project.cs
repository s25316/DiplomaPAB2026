using Diploma.Database.Models.Projects.ProjectEvents;

namespace Diploma.Database.Models.Projects;

public class Project
{
    public Guid ProjectId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsVisible { get; set; } = false;


    public Guid? RootId { get; set; } = null;
    public virtual Project? Root { get; set; } = null;
    public virtual ICollection<Project> History { get; set; } = [];

    public Guid? NextId { get; set; }
    public virtual Project? Next { get; set; } = null;
    public virtual Project? Previous { get; set; } = null;

    public virtual ICollection<ProjectEvent> ProjectEvents { get; set; } = [];
}