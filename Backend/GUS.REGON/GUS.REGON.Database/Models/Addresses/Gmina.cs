namespace GUS.REGON.Database.Models.Addresses;

public class Gmina
{
    public string GminaId { get; set; } = null!;
    public string Name { get; set; } = null!;

    public virtual ICollection<Address> Addresses { get; set; } = [];
}