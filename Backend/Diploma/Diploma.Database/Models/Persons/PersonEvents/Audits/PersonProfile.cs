namespace Diploma.Database.Models.Persons.PersonEvents.Audits;

public class PersonProfile
{
    public Guid PersonProfileId { get; set; }
    public string? Summary { get; set; }
    public string? Title { get; set; }


    public Guid PersonEventId { get; set; }
    public virtual PersonEvent PersonEvent { get; set; } = null!;
}