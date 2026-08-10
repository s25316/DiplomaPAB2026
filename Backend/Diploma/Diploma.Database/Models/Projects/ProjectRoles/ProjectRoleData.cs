using Diploma.Database.Models.Projects.ProjectEvents;

namespace Diploma.Database.Models.Projects.ProjectRoles;

public class ProjectRoleData
{
    public Guid ProjectRoleDataId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsAvailableRecruitment { get; set; } = false;

    public Guid ProjectRoleId { get; set; }
    public virtual ProjectRole ProjectRole { get; set; } = null!;

    public Guid ProjectEventId { get; set; }
    public virtual ProjectEvent ProjectEvent { get; set; } = null!;


    public Guid? RootId { get; set; } = null;
    public virtual ProjectRoleData? Root { get; set; } = null;
    public virtual ICollection<ProjectRoleData> History { get; set; } = [];

    public Guid? NextId { get; set; }
    public virtual ProjectRoleData? Next { get; set; } = null;
    public virtual ProjectRoleData? Previous { get; set; } = null;

    public virtual ICollection<ProjectRole> ProjectRoles { get; set; } = [];
}
