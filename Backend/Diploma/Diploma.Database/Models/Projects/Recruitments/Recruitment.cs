using Diploma.Database.Models.Persons;

namespace Diploma.Database.Models.Projects.Recruitments;

public class Recruitment
{
    public Guid RecruitmentId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }


    public Guid PersonId { get; set; }
    public virtual Person Person { get; set; } = null!;

    public Guid ProjectId { get; set; }
    public virtual Project Project { get; set; } = null!;

    public Guid? LastRecruitmentStatusAuditId { get; set; } = null;
    public virtual RecruitmentStatusAudit? LastRecruitmentStatusAudit { get; set; } = null;

    public virtual ICollection<RecruitmentMessage> RecruitmentMessages { get; set; } = [];
    public virtual ICollection<RecruitmentProjectRole> RecruitmentProjectRoles { get; set; } = [];
    public virtual ICollection<RecruitmentStatusAudit> RecruitmentStatusAudits { get; set; } = [];
}