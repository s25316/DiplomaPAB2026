// Ignore Spelling: Gmina, Gminy, Rodz

namespace GUS.TERYT.Database.Models.Tercs;

public class GminaRodz
{
    public string GminaRodzCode { get; set; } = null!;
    public string Name { get; set; } = null!;


    public virtual ICollection<Gmina> Gminy { get; set; } = [];
}