// Ignore Spelling: Simc, Ulic, Miejscowosc
using GUS.TERYT.Database.Models.Simcs;
using GUS.TERYT.Database.Models.Ulics;

namespace GUS.TERYT.Database.Models;

public class SimcUlic
{
    public Guid ConnectionId { get; set; }
    public string MiejscowoscId { get; set; } = null!;
    public string UlicId { get; set; } = null!;


    public virtual Simc Miejscowosc { get; set; } = null!;
    public virtual Ulic Ulic { get; set; } = null!;
}
