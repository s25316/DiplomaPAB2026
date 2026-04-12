using GUS.REGON.Database.Models;
using GUS.REGON.Database.Models.Addresses;
using GUS.REGON.Database.Models.Contacts;
using GUS.REGON.Database.Models.RegistrationDetails;
using GUS.REGON.Database.MsSql.Configurations;
using GUS.REGON.Database.MsSql.Configurations.Addresses;
using GUS.REGON.Database.MsSql.Configurations.Contacts;
using GUS.REGON.Database.MsSql.Configurations.RegistrationDetails;
using Microsoft.EntityFrameworkCore;

namespace GUS.REGON.Database.MsSql;

/*
PowerShell
dotnet tool install --global dotnet-ef

dotnet add GUS.REGON.Database.MsSql package Microsoft.EntityFrameworkCore.Design

dotnet ef migrations add Hand `
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
        modelBuilder.ApplyConfiguration<TypJednostki>(new TypJednostkiEFConfiguration());

        modelBuilder.ApplyConfiguration<Address>(new AddressEFConfiguration());
        modelBuilder.ApplyConfiguration<Kraj>(new KrajEFConfiguration());
        modelBuilder.ApplyConfiguration<Wojewodztwo>(new WojewodztwoEFConfiguration());
        modelBuilder.ApplyConfiguration<Powiat>(new PowiatEFConfiguration());
        modelBuilder.ApplyConfiguration<Gmina>(new GminaEFConfiguration());
        modelBuilder.ApplyConfiguration<MiejscowoscPoczty>(new MiejscowoscPocztyEFConfiguration());
        modelBuilder.ApplyConfiguration<Miejscowosc>(new MiejscowoscEFConfiguration());
        modelBuilder.ApplyConfiguration<Ulica>(new UlicaEFConfiguration());

        modelBuilder.ApplyConfiguration<FormaFinansowania>(new FormaFinansowaniaEFConfiguration());
        modelBuilder.ApplyConfiguration<FormaWlasnosci>(new FormaWlasnosciEFConfiguration());
        modelBuilder.ApplyConfiguration<OrganRejestrowy>(new OrganRejestrowyEFConfiguration());
        modelBuilder.ApplyConfiguration<OrganZalozycielski>(new OrganZalozycielskiEFConfiguration());
        modelBuilder.ApplyConfiguration<PodstawowaFormaPrawna>(new PodstawowaFormaPrawnaEFConfiguration());
        modelBuilder.ApplyConfiguration<SzczegolnaFormaPrawna>(new SzczegolnaFormaPrawnaEFConfiguration());
        modelBuilder.ApplyConfiguration<RodzajRejestru>(new RodzajRejestruEFConfiguration());

        modelBuilder.ApplyConfiguration<PhoneNumber>(new PhoneNumberEFConfiguration());
        modelBuilder.ApplyConfiguration<Website>(new WebsiteEFConfiguration());
        modelBuilder.ApplyConfiguration<Email>(new EmailEFConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}