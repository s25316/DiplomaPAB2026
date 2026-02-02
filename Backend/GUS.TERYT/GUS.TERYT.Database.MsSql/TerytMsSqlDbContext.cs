// Ignore Spelling: Teryt
using GUS.TERYT.Database.Models;
using GUS.TERYT.Database.Models.Simcs;
using GUS.TERYT.Database.Models.Tercs;
using GUS.TERYT.Database.Models.Ulicy;
using GUS.TERYT.Database.MsSql.Configurations;
using GUS.TERYT.Database.MsSql.Configurations.Simcs;
using GUS.TERYT.Database.MsSql.Configurations.Tercs;
using GUS.TERYT.Database.MsSql.Configurations.Ulicy;
using Microsoft.EntityFrameworkCore;

namespace GUS.TERYT.Database.MsSql;

// Add-Migration First -Project GUS.TERYT.Database.MsSql -Context TerytMsSqlDbContext
// Update-Database -Project GUS.TERYT.Database.MsSql -Context TerytMsSqlDbContext
public class TerytMsSqlDbContext(DbContextOptions options) : TerytDbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration<Wojewodztwo>(new WojewodztwoEFConfiguration());
        modelBuilder.ApplyConfiguration<Powiat>(new PowiatEFConfiguration());
        modelBuilder.ApplyConfiguration<PowiatType>(new PowiatTypeEFConfiguration());
        modelBuilder.ApplyConfiguration<Gmina>(new GminaEFConfiguration());
        modelBuilder.ApplyConfiguration<GminaRodz>(new GminaRodzEFConfiguration());

        modelBuilder.ApplyConfiguration<Simc>(new SimcEFConfiguration());
        modelBuilder.ApplyConfiguration<SimcType>(new SimcTypeEFConfiguration());

        modelBuilder.ApplyConfiguration<Ulica>(new UlicaEFConfiguration());
        modelBuilder.ApplyConfiguration<UlicaType>(new UlicaTypeEFConfiguration());

        modelBuilder.ApplyConfiguration<SimcUlica>(new SimcUlicaEFConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
