namespace GUS.REGON.Database.Models.RegistrationDetails;

public class FormaWlasnosci
{
    public string FormaWlasnosciId { get; set; } = null!;
    public string Name { get; set; } = null!;

    public virtual ICollection<Report> Reports { get; set; } = [];
}