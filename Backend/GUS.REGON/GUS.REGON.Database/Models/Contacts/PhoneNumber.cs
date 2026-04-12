namespace GUS.REGON.Database.Models.Contacts;

public class PhoneNumber
{
    public Guid PhoneNumberId { get; set; }
    public required string Value { get; set; } = null!;

    public virtual ICollection<Report> Reports { get; set; } = [];
}