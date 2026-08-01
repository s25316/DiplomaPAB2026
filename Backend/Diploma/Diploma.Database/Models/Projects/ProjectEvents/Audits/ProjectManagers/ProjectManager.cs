using Diploma.Database.Models.Persons;

namespace Diploma.Database.Models.Projects.ProjectEvents.Audits.ProjectManagers;

public class ProjectManager
{
    public Guid ProjectManagerId { get; set; }


    public Guid PersonId { get; set; }
    public virtual Person Person { get; set; } = null!;

    public int ProjectManagerTypeId { get; set; }
    public virtual ProjectManagerType ProjectManagerType { get; set; } = null!;

    public Guid GrantEventId { get; set; }
    public virtual ProjectEvent GrantEvent { get; set; } = null!;

    public Guid? RevokeEventId { get; set; } = null;
    public virtual ProjectEvent? RevokeEvent { get; set; } = null!;
}