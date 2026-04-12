namespace GUS.REGON.Database.Models.RegistrationDetails;

public class OrganZalozycielski
{
    public string OrganZalozycielskiId { get; set; } = null!;
    public string Name { get; set; } = null!;

    public virtual ICollection<Report> Reports { get; set; } = [];
}