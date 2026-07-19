namespace Diploma.Database.Models.Persons.PersonEvents.Audits;

public class PersonIdentity
{
    public Guid PersonIdentityId { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;


    public Guid PersonEventId { get; set; }
    public virtual PersonEvent PersonEvent { get; set; } = null!;
}