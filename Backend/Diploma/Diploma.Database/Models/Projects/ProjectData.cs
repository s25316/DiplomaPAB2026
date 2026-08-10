using Diploma.Database.Models.Projects.ProjectEvents;

namespace Diploma.Database.Models.Projects;

public class ProjectData
{
    public Guid ProjectDataId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsVisible { get; set; } = false;


    public Guid? RootId { get; set; } = null;
    public virtual ProjectData? Root { get; set; } = null;
    public virtual ICollection<ProjectData> History { get; set; } = [];

    public Guid? NextId { get; set; }
    public virtual ProjectData? Next { get; set; } = null;
    public virtual ProjectData? Previous { get; set; } = null;


    public Guid ProjectEventId { get; set; }
    public virtual ProjectEvent ProjectEvent { get; set; } = null!;


    public virtual ICollection<Project> Projects { get; set; } = [];
}