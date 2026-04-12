namespace GUS.REGON.Database.Models.Addresses;

public class MiejscowoscPoczty
{
    public string MiejscowoscPocztyId { get; set; } = null!;
    public string Name { get; set; } = null!;

    public virtual ICollection<Address> Addresses { get; set; } = [];
}