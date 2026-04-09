namespace GUS.REGON.Database.Models.RegistrationDetails;

public class SzczegolnaFormaPrawna
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;

    public virtual ICollection<Report> Reports { get; set; } = [];
}