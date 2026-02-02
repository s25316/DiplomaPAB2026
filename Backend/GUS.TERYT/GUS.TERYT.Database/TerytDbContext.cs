// Ignore Spelling: Teryt, Wojewodstwa, Powiat, Powiaty, Gminy, Gmina, Rodzaje, Miejscowosci, Miejscowosc
// Ignore Spelling: Ulicy, Ulic, Simc Ulics
using GUS.TERYT.Database.Models;
using GUS.TERYT.Database.Models.Simcs;
using GUS.TERYT.Database.Models.Tercs;
using GUS.TERYT.Database.Models.Ulicy;
using Microsoft.EntityFrameworkCore;

namespace GUS.TERYT.Database;

public class TerytDbContext(DbContextOptions options) : DbContext(options)
{
    public virtual DbSet<Wojewodztwo> Wojewodztwa { get; set; }
    public virtual DbSet<Powiat> Powiaty { get; set; }
    public virtual DbSet<PowiatType> PowiatTypes { get; set; }
    public virtual DbSet<Gmina> Gminy { get; set; }
    public virtual DbSet<GminaRodz> GminaRodzaje { get; set; }

    public virtual DbSet<Simc> Miejscowosci { get; set; }
    public virtual DbSet<SimcType> MiejscowoscRodzaje { get; set; }

    public virtual DbSet<Ulica> Ulicy { get; set; }
    public virtual DbSet<UlicaType> UlicTypes { get; set; }

    public virtual DbSet<SimcUlica> SimcUlics { get; set; }
}
