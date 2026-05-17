namespace GUS.REGON.Database.Models.Addresses;

public class Miejscowosc
{
    public string MiejscowoscCode { get; set; } = null!;
    public string Name { get; set; } = null!;

    public virtual ICollection<Address> Addresses { get; set; } = [];
}