// Ignore Spelling: Simc, Rodzaj, Miejscowosci

namespace GUS.TERYT.Database.Models.Simcs;

public class SimcType
{
    public string TypeCode { get; set; } = null!;
    public string Name { get; set; } = null!;


    public virtual ICollection<Simc> Miejscowosci { get; set; } = [];
}
