// Ignore Spelling: Wojewodztwo, Powiat, Powiaty

namespace GUS.TERYT.Database.Models.Tercs;

public class Wojewodztwo
{
    public string WojewodztwoCode { get; set; } = null!;
    public string Name { get; set; } = null!;


    public virtual ICollection<Powiat> Powiaty { get; set; } = [];
}