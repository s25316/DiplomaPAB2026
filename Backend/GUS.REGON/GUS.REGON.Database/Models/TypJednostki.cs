namespace GUS.REGON.Database.Models;

public class TypJednostki
{
    public string TypJednostkiCode { get; set; } = null!;
    public string Name { get; set; } = null!;

    public virtual ICollection<Institution> Institutions { get; set; } = [];
}