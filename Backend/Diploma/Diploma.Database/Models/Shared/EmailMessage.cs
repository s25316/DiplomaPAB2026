using Diploma.Database.Models.Persons.PersonOperations;

namespace Diploma.Database.Models.Shared;

public class EmailMessage
{
    public Guid EmailMessageId { get; set; }
    public string Email { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Body { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? DeliveredAt { get; set; }


    public Guid PersonOperationId { get; set; }
    public virtual PersonOperation PersonOperation { get; set; } = null!;
}