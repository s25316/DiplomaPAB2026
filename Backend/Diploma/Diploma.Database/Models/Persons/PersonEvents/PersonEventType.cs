namespace Diploma.Database.Models.Persons.PersonEvents;

public class PersonEventType
{
    public int PersonEventTypeId { get; set; }
    public string Name { get; set; } = null!;


    public virtual ICollection<PersonEvent> PersonEvents { get; set; } = [];
}