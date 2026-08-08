using Diploma.Database.Models.Persons;

namespace Diploma.Database.Models.Projects.Recruitments;

public class Recruitment
{
    public Guid RecruitmentId { get; set; }

    public Guid PersonId { get; set; }
    public virtual Person Person { get; set; } = null!;

    public int RecruitmentStatusId { get; set; }
    public RecruitmentStatus RecruitmentStatus { get; set; } = null!;

    public Guid? RootId { get; set; } = null;
    public virtual Recruitment? Root { get; set; } = null;
    public virtual ICollection<Recruitment> History { get; set; } = [];

    public Guid? NextId { get; set; }
    public virtual Recruitment? Next { get; set; } = null;
    public virtual Recruitment? Previous { get; set; } = null;

    public virtual ICollection<RecruitmentMessage> RecruitmentMessages { get; set; } = [];
    public virtual ICollection<RecruitmentProjectRole> RecruitmentProjectRoles { get; set; } = [];
}