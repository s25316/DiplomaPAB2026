namespace GUS.REGON.Database.Models.Addresses;

public class Powiat
{
    public string PowiatCode { get; set; } = null!;
    public string Name { get; set; } = null!;

    public virtual ICollection<Address> Addresses { get; set; } = [];
}