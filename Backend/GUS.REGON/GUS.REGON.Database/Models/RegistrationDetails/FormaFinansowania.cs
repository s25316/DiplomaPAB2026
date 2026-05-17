namespace GUS.REGON.Database.Models.RegistrationDetails;

public class FormaFinansowania
{
    public string FormaFinansowaniaCode { get; set; } = null!;
    public string Name { get; set; } = null!;

    public virtual ICollection<Institution> Institutions { get; set; } = [];
}