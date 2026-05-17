namespace GUS.REGON.Database.Models.RegistrationDetails;

public class OrganRejestrowy
{
    public string OrganRejestrowyCode { get; set; } = null!;
    public string Name { get; set; } = null!;

    public virtual ICollection<Institution> Institutions { get; set; } = [];
}