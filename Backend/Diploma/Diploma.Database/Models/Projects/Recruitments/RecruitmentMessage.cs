using Diploma.Database.Models.Persons;

namespace Diploma.Database.Models.Projects.Recruitments;

public class RecruitmentMessage
{
    public Guid RecruitmentMessageId { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public string Message { get; set; } = null!;
    public string? File { get; set; } = null!;

    public Guid RecruitmentId { get; set; }
    public virtual Recruitment Recruitment { get; set; } = null!;

    public Guid PersonId { get; set; }
    public virtual Person Person { get; set; } = null!;
}