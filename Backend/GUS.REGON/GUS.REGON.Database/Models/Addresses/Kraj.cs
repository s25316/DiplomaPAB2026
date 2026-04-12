namespace GUS.REGON.Database.Models.Addresses;

public class Kraj
{
    public string KrajId { get; set; } = null!;
    public string Name { get; set; } = null!;

    public virtual ICollection<Address> Addresses { get; set; } = [];
}