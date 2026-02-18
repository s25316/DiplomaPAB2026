// Ignore Spelling: Wojewodstwo, Powiat, Gminy, Gmina, Rodz, Miejscowosci
using GUS.TERYT.Database.Models.Simcs;

namespace GUS.TERYT.Database.Models.Tercs;

public class Gmina
{
    public Guid GminaId { get; set; }
    public string GminaCode { get; set; } = null!;
    public string GminaRodzCode { get; set; } = null!;
    public string Name { get; set; } = null!;
    public Guid PowiatId { get; set; }


    public virtual GminaRodz Rodz { get; set; } = null!;
    public virtual Powiat Powiat { get; set; } = null!;
    public virtual ICollection<Simc> Miejscowosci { get; set; } = [];
}