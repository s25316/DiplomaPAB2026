namespace GUS.REGON.Database.Models.RegistrationDetails;

public class FormaWlasnosci
{
    public string FormaWlasnosciCode { get; set; } = null!;
    public string Name { get; set; } = null!;

    public virtual ICollection<Institution> Institutions { get; set; } = [];
}