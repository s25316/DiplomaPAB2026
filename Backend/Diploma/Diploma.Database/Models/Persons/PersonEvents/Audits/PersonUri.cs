namespace Diploma.Database.Models.Persons.PersonEvents.Audits;

public class PersonUri
{
    public Guid PersonUriId { get; set; }
    public string Uri { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;


    public Guid PersonEventId { get; set; }
    public virtual PersonEvent PersonEvent { get; set; } = null!;

    public Guid? RootId { get; set; } = null;
    public virtual PersonUri? Root { get; set; } = null;
    public virtual ICollection<PersonUri> History { get; set; } = [];

    public Guid? NextId { get; set; }
    public virtual PersonUri? Next { get; set; } = null;
    public virtual PersonUri? Previous { get; set; } = null;
}