namespace GUS.REGON.Database.Models.RegistrationDetails;

public class RodzajRejestru
{
    public string RodzajRejestruCode { get; set; } = null!;
    public string Name { get; set; } = null!;

    public virtual ICollection<Institution> Institutions { get; set; } = [];
}