namespace GUS.REGON.Database.Models.RegistrationDetails;

public class PodstawowaFormaPrawna
{
    public string PodstawowaFormaPrawnaCode { get; set; } = null!;
    public string Name { get; set; } = null!;

    public virtual ICollection<Institution> Institutions { get; set; } = [];
}