namespace GUS.REGON.Database.Models.RegistrationDetails;

public class SzczegolnaFormaPrawna
{
    public string SzczegolnaFormaPrawnaId { get; set; } = null!;
    public string Name { get; set; } = null!;

    public virtual ICollection<Report> Reports { get; set; } = [];
}