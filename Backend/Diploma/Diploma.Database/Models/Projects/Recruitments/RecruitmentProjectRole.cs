using Diploma.Database.Models.Projects.ProjectRoles;

namespace Diploma.Database.Models.Projects.Recruitments;

public class RecruitmentProjectRole
{
    public Guid RecruitmentProjectRoleId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }


    public Guid RecruitmentId { get; set; }
    public virtual Recruitment Recruitment { get; set; } = null!;

    public Guid ProjectRoleId { get; set; }
    public virtual ProjectRole ProjectRole { get; set; } = null!;
}