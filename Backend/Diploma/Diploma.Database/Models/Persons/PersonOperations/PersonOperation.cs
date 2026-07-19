using Diploma.Database.Models.Shared;

namespace Diploma.Database.Models.Persons.PersonOperations;

public class PersonOperation
{
    public Guid PersonOperationId { get; set; }
    public string? Value { get; set; } = null;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }
    public DateTimeOffset? ActivatedAt { get; set; }


    public int PersonOperationTypeId { get; set; }
    public virtual PersonOperationType PersonOperationType { get; set; } = null!;

    public int VerificationMethodId { get; set; }
    public virtual VerificationMethod VerificationMethod { get; set; } = null!;

    public Guid PersonId { get; set; }
    public virtual Person Person { get; set; } = null!;

    public virtual ICollection<EmailMessage> EmailMessages { get; set; } = [];
}