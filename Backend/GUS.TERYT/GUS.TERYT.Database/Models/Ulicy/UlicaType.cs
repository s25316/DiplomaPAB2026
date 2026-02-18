// Ignore Spelling: Ulica, Ulicy

namespace GUS.TERYT.Database.Models.Ulicy;

public class UlicaType
{
    public int TypeCode { get; set; }
    public string Name { get; set; } = null!;


    public virtual ICollection<Ulica> Ulicy { get; set; } = [];
}