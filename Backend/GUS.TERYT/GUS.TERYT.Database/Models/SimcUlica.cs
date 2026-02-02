// Ignore Spelling: Simc, Ulica, Miejscowosc
using GUS.TERYT.Database.Models.Simcs;
using GUS.TERYT.Database.Models.Ulicy;

namespace GUS.TERYT.Database.Models;

public class SimcUlica
{
    public Guid ConnectionId { get; set; }
    public string MiejscowoscCode { get; set; } = null!;
    public string UlicaCode { get; set; } = null!;


    public virtual Simc Miejscowosc { get; set; } = null!;
    public virtual Ulica Ulica { get; set; } = null!;
}
