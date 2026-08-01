namespace Diploma.Database.Models.Projects.ProjectEvents.Audits.ProjectManagers;

public class ProjectManagerType
{
    public int ProjectManagerTypeId { get; set; }
    public string Name { get; set; } = null!;


    public virtual ICollection<ProjectManager> ProjectManagers { get; set; } = [];
}