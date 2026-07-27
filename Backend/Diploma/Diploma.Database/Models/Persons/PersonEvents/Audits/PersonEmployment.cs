namespace Diploma.Database.Models.Persons.PersonEvents.Audits;

public class PersonEmployment
{
    public Guid PersonEmploymentId { get; set; }
    public string Regon { get; set; } = null!;
    public string Position { get; set; } = null!;
    public string Descrition { get; set; } = null!;
    public DateOnly From { get; set; }
    public DateOnly? To { get; set; } = null;


    public Guid PersonEventId { get; set; }
    public virtual PersonEvent PersonEvent { get; set; } = null!;

    public Guid? RootId { get; set; } = null;
    public virtual PersonEmployment? Root { get; set; } = null;
    public virtual ICollection<PersonEmployment> History { get; set; } = [];

    public Guid? NextId { get; set; }
    public virtual PersonEmployment? Next { get; set; } = null;
    public virtual PersonEmployment? Previous { get; set; } = null;
}