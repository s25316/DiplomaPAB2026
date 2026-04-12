namespace GUS.REGON.Database.Models.Contacts;

public class Website
{
    public Guid WebsiteId { get; set; }
    public required string Value { get; set; } = null!;

    public virtual ICollection<Report> Reports { get; set; } = [];
}