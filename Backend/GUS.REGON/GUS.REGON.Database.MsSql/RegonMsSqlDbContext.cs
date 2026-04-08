using GUS.REGON.Database.Models;
using GUS.REGON.Database.Models.Addresses;
using GUS.REGON.Database.MsSql.Configurations;
using GUS.REGON.Database.MsSql.Configurations.Addresses;
using Microsoft.EntityFrameworkCore;

namespace GUS.REGON.Database.MsSql;

/*
PowerShell
dotnet tool install --global dotnet-ef

dotnet add GUS.REGON.Database.MsSql package Microsoft.EntityFrameworkCore.Design

dotnet ef migrations add First `
  --project GUS.REGON.Database.MsSql `
  --startup-project GUS.REGON.API `
  --context RegonMsSqlDbContext `
  --framework net10.0

dotnet ef database update `
  --project GUS.REGON.Database.MsSql `
  --startup-project GUS.REGON.API `
  --context RegonMsSqlDbContext `
  --framework net10.0
 */

// Add-Migration First -Project GUS.REGON.Database.MsSql -Context RegonMsSqlDbContext
// Update-Database -Project GUS.REGON.Database.MsSql -Context RegonMsSqlDbContext
public class RegonMsSqlDbContext(DbContextOptions options) : RegonDbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration<Query>(new QueryEFConfiguration());
        modelBuilder.ApplyConfiguration<Report>(new ReportEFConfiguration());

        modelBuilder.ApplyConfiguration<Address>(new AddressEFConfiguration());
        modelBuilder.ApplyConfiguration<Kraj>(new KrajEFConfiguration());
        modelBuilder.ApplyConfiguration<Wojewodztwo>(new WojewodztwoEFConfiguration());
        modelBuilder.ApplyConfiguration<Powiat>(new PowiatEFConfiguration());
        modelBuilder.ApplyConfiguration<Gmina>(new GminaEFConfiguration());
        modelBuilder.ApplyConfiguration<MiejscowoscPoczty>(new MiejscowoscPocztyEFConfiguration());
        modelBuilder.ApplyConfiguration<Miejscowosc>(new MiejscowoscEFConfiguration());
        modelBuilder.ApplyConfiguration<Ulica>(new UlicaEFConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}