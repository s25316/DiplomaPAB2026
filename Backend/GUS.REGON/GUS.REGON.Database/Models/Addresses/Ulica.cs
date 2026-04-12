namespace GUS.REGON.Database.Models.Addresses;

public class Ulica
{
    public string UlicaId { get; set; } = null!;
    public string Name { get; set; } = null!;

    public virtual ICollection<Address> Addresses { get; set; } = [];
}