// Ignore Spelling: Teryt, Wojewodstwa, Powiat, Powiaty, Gminy, Gmina, Rodzaje, Miejscowosci, Miejscowosc
// Ignore Spelling: Ulicy, Ulic, Simc Ulics
using GUS.TERYT.Database.Models;
using GUS.TERYT.Database.Models.Simcs;
using GUS.TERYT.Database.Models.Tercs;
using GUS.TERYT.Database.Models.Ulics;
using Microsoft.EntityFrameworkCore;

namespace GUS.TERYT.Database;

public class TerytDbContext : DbContext
{
    public virtual DbSet<Wojewodstwo> Wojewodstwa { get; set; }
    public virtual DbSet<Powiat> Powiaty { get; set; }
    public virtual DbSet<PowiatType> PowiatTypes { get; set; }
    public virtual DbSet<Gmina> Gminy { get; set; }
    public virtual DbSet<GminaRodz> GminaRodzaje { get; set; }

    public virtual DbSet<Simc> Miejscowosci { get; set; }
    public virtual DbSet<SimcRodzaj> MiejscowoscRodzaje { get; set; }

    public virtual DbSet<Ulic> Ulicy { get; set; }
    public virtual DbSet<UlicType> UlicTypes { get; set; }

    public virtual DbSet<SimcUlic> SimcUlics { get; set; }
}
