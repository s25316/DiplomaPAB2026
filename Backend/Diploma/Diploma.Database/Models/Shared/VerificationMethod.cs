using Diploma.Database.Models.Persons.PersonOperations;

namespace Diploma.Database.Models.Shared;

public class VerificationMethod
{
    public int VerificationMethodId { get; set; }
    public string Name { get; set; } = null!;


    public virtual ICollection<PersonOperation> PersonOperations { get; set; } = [];
}