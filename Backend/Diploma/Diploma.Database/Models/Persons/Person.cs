using Diploma.Database.Models.Persons.PersonEvents;
using Diploma.Database.Models.Persons.PersonOperations;
using Diploma.Database.Models.Projects.ProjectEvents;
using Diploma.Database.Models.Projects.ProjectManagers;
using Diploma.Database.Models.Projects.Recruitments;

namespace Diploma.Database.Models.Persons;

public class Person
{
    public Guid PersonId { get; set; }
    public string? Login { get; set; }
    public string? Password { get; set; }
    public string? Salt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? ActivatedAt { get; set; }
    public DateTimeOffset? RemovedAt { get; set; }
    public DateTimeOffset? AnonymizedAt { get; set; }


    public virtual ICollection<PersonEvent> PersonEvents { get; set; } = [];
    public virtual ICollection<PersonOperation> PersonOperations { get; set; } = [];
    public virtual ICollection<ProjectEvent> ProjectEvents { get; set; } = [];
    public virtual ICollection<ProjectManager> ProjectManagers { get; set; } = [];
    public virtual ICollection<Recruitment> Recruitments { get; set; } = [];
    public virtual ICollection<RecruitmentMessage> RecruitmentMessages { get; set; } = [];
    public virtual ICollection<RecruitmentStatusAudit> RecruitmentStatusAudits { get; set; } = [];
}