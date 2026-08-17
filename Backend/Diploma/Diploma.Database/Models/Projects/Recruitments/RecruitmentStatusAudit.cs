using Diploma.Database.Models.Persons;

namespace Diploma.Database.Models.Projects.Recruitments;

public class RecruitmentStatusAudit
{
    public Guid RecruitmentStatusAuditId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }


    public Guid RecruitmentId { get; set; }
    public virtual Recruitment Recruitment { get; set; } = null!;

    public int RecruitmentStatusId { get; set; }
    public virtual RecruitmentStatus RecruitmentStatus { get; set; } = null!;

    public Guid PersonId { get; set; }
    public virtual Person Person { get; set; } = null!;


    public Guid? RootId { get; set; } = null;
    public virtual RecruitmentStatusAudit? Root { get; set; } = null;
    public virtual ICollection<RecruitmentStatusAudit> History { get; set; } = [];

    public Guid? NextId { get; set; }
    public virtual RecruitmentStatusAudit? Next { get; set; } = null;
    public virtual RecruitmentStatusAudit? Previous { get; set; } = null;

    public virtual ICollection<Recruitment> Recruitments { get; set; } = [];
}
