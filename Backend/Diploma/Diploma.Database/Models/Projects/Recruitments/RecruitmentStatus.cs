namespace Diploma.Database.Models.Projects.Recruitments;

public class RecruitmentStatus
{
    public int RecruitmentStatusId { get; set; }
    public string Name { get; set; } = null!;


    public virtual ICollection<RecruitmentStatusAudit> RecruitmentStatusAudits { get; set; } = [];
}