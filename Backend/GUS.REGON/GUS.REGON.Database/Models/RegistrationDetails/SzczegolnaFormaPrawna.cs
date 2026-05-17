namespace GUS.REGON.Database.Models.RegistrationDetails;

public class SzczegolnaFormaPrawna
{
    public string SzczegolnaFormaPrawnaCode { get; set; } = null!;
    public string Name { get; set; } = null!;

    public virtual ICollection<Institution> Institutions { get; set; } = [];
}