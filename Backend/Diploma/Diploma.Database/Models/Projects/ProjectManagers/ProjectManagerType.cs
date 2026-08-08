namespace Diploma.Database.Models.Projects.ProjectManagers;

public class ProjectManagerType
{
    public int ProjectManagerTypeId { get; set; }
    public string Name { get; set; } = null!;


    public virtual ICollection<ProjectManager> ProjectManagers { get; set; } = [];
}