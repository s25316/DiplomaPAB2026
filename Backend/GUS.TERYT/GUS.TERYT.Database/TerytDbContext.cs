// Ignore Spelling: Teryt, Wojewodztwa, Powiat, Powiaty, Gminy, Gmina, Rodzaje, Miejscowosci, Miejscowosc
// Ignore Spelling: Ulicy, Ulic, Simc Ulics
using GUS.TERYT.Database.Models;
using GUS.TERYT.Database.Models.Simcs;
using GUS.TERYT.Database.Models.Tercs;
using GUS.TERYT.Database.Models.Ulicy;
using Microsoft.EntityFrameworkCore;

namespace GUS.TERYT.Database;

public class TerytDbContext(DbContextOptions options) : DbContext(options)
{
    public virtual DbSet<Wojewodztwo> Wojewodztwa { get; set; } = null!;
    public virtual DbSet<Powiat> Powiaty { get; set; } = null!;
    public virtual DbSet<PowiatType> PowiatTypes { get; set; } = null!;
    public virtual DbSet<Gmina> Gminy { get; set; } = null!;
    public virtual DbSet<GminaRodz> GminaRodzaje { get; set; } = null!;

    public virtual DbSet<Simc> Miejscowosci { get; set; } = null!;
    public virtual DbSet<SimcType> MiejscowoscRodzaje { get; set; } = null!;

    public virtual DbSet<Ulica> Ulicy { get; set; } = null!;
    public virtual DbSet<UlicaType> UlicTypes { get; set; } = null!;

    public virtual DbSet<SimcUlica> SimcUlics { get; set; } = null!;
}
