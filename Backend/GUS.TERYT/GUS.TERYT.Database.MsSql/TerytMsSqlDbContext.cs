// Ignore Spelling: Teryt
using GUS.TERYT.Database.Models;
using GUS.TERYT.Database.Models.Simcs;
using GUS.TERYT.Database.Models.Tercs;
using GUS.TERYT.Database.Models.Ulics;
using GUS.TERYT.Database.MsSql.Configurations;
using GUS.TERYT.Database.MsSql.Configurations.Simcs;
using GUS.TERYT.Database.MsSql.Configurations.Tercs;
using GUS.TERYT.Database.MsSql.Configurations.Ulics;
using Microsoft.EntityFrameworkCore;

namespace GUS.TERYT.Database.MsSql;

// Add-Migration First -Project GUS.TERYT.Database.MsSql -Context TerytMsSqlDbContext
// Update-Database -Project GUS.TERYT.Database.MsSql -Context TerytMsSqlDbContext
public class TerytMsSqlDbContext : TerytDbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfiguration<Wojewodstwo>(new WojewodstwoEFConfiguration());
        modelBuilder.ApplyConfiguration<Powiat>(new PowiatEFConfiguration());
        modelBuilder.ApplyConfiguration<PowiatType>(new PowiatTypeEFConfiguration());
        modelBuilder.ApplyConfiguration<Gmina>(new GminaEFConfiguration());
        modelBuilder.ApplyConfiguration<GminaRodz>(new GminaRodzEFConfiguration());

        modelBuilder.ApplyConfiguration<Simc>(new SimcEFConfiguration());
        modelBuilder.ApplyConfiguration<SimcRodzaj>(new SimcRodzajEFConfiguration());

        modelBuilder.ApplyConfiguration<Ulic>(new UlicEFConfiguration());
        modelBuilder.ApplyConfiguration<UlicType>(new UlicTypeEFConfiguration());

        modelBuilder.ApplyConfiguration<SimcUlic>(new SimcUlicEFConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=Diploma;User ID=sa;Password=YourStrong!Passw0rd;Trust Server Certificate=True");
        base.OnConfiguring(optionsBuilder);
    }
}
