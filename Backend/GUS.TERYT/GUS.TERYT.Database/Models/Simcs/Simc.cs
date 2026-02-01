// Ignore spelling: Simc, Miejscowosc, Rodzaj, Gmina, Simc, Ulics
using GUS.TERYT.Database.Models.Tercs;

namespace GUS.TERYT.Database.Models.Simcs;

public class Simc
{
    public string MiejscowoscId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string RodzajCode { get; set; } = null!;
    public Guid GminaId { get; set; }


    public virtual SimcRodzaj Rodzaj { get; set; } = null!;
    public virtual Gmina Gmina { get; set; } = null!;
    public virtual ICollection<SimcUlic> SimcUlics { get; set; } = [];
}
