// Ignore Spelling: Simc, Rodzaj, Miejscowosci

namespace GUS.TERYT.Database.Models.Simcs;

public class SimcRodzaj
{
    public string RodzajCode { get; set; } = null!;
    public string Name { get; set; } = null!;


    public virtual ICollection<Simc> Miejscowosci { get; set; } = [];
}
