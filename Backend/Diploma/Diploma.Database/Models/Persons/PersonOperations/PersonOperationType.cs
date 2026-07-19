namespace Diploma.Database.Models.Persons.PersonOperations;

public class PersonOperationType
{
    public int PersonOperationTypeId { get; set; }
    public string Name { get; set; } = null!;


    public virtual ICollection<PersonOperation> PersonOperations { get; set; } = [];
}