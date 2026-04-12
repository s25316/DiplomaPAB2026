namespace GUS.REGON.Database.Models.Contacts;

public class Email
{
    public Guid EmailId { get; set; }
    public required string Value { get; set; } = null!;

    public virtual ICollection<Report> Reports { get; set; } = [];
}