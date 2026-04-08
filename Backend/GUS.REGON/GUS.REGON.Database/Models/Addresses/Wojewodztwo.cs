namespace GUS.REGON.Database.Models.Addresses;

public class Wojewodztwo
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;

    public virtual ICollection<Address> Addresses { get; set; } = [];
}