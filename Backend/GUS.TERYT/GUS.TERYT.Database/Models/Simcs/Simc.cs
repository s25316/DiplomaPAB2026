// Ignore spelling: Simc, Simcs, Miejscowosc, Rodzaj, Gmina, Simc, Ulicy
using GUS.TERYT.Database.Models.Tercs;

namespace GUS.TERYT.Database.Models.Simcs;

public class Simc
{
    public string MiejscowoscCode { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string TypeCode { get; set; } = null!;
    public Guid GminaId { get; set; }


    public virtual SimcType Type { get; set; } = null!;
    public virtual Gmina Gmina { get; set; } = null!;
    public virtual ICollection<SimcUlica> SimcUlicy { get; set; } = [];
}
