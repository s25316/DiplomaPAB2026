// Ignore Spelling: Powiat, Powiaty

namespace GUS.TERYT.Database.Models.Tercs;

public class PowiatType
{
    public int TypeCode { get; set; }
    public string Name { get; set; } = null!;


    public virtual ICollection<Powiat> Powiaty { get; set; } = [];
}