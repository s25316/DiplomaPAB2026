// Ignore Spelling: Wojewodztwo, Powiat, Gminy

namespace GUS.TERYT.Database.Models.Tercs;

public class Powiat
{
    public Guid PowiatId { get; set; }
    public string WojewodztwoCode { get; set; } = null!;
    public string PowiatCode { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int TypeCode { get; set; }


    public virtual PowiatType Type { get; set; } = null!;
    public virtual Wojewodztwo Wojewodztwo { get; set; } = null!;
    public virtual ICollection<Gmina> Gminy { get; set; } = [];
}
