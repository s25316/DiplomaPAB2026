// Ignore Spelling: Simc, Ulica

namespace GUS.TERYT.Database.Models.Ulicy;

public class Ulica
{
    public string UlicaCode { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int? TypeCode { get; set; } = null;


    public virtual UlicaType? Type { get; set; } = null;
    public virtual ICollection<SimcUlica> SimcUlica { get; set; } = [];
}