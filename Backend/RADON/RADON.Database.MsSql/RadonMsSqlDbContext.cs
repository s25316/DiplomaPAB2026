using Microsoft.EntityFrameworkCore;
using RADON.Database.Models;
using RADON.Database.Models.Institutions;
using RADON.Database.MsSql.Configurations;
using RADON.Database.MsSql.Configurations.Institutions;

namespace RADON.Database.MsSql;

/*
PowerShell
dotnet tool install --global dotnet-ef

dotnet add RADON.Database.MsSql package Microsoft.EntityFrameworkCore.Design

dotnet ef migrations add First `
  --project RADON.Database.MsSql `
  --startup-project RADON.API `
  --context RadonMsSqlDbContext `
  --framework net10.0

dotnet ef database update `
  --project RADON.Database.MsSql `
  --startup-project RADON.API `
  --context RadonMsSqlDbContext `
  --framework net10.0
 */
public class RadonMsSqlDbContext(DbContextOptions options) : RadonDbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration<DataSource>(new DataSourceEFConfiguration());


        // --- INSTITUTIONS ---
        modelBuilder.ApplyConfiguration<Institution>(new InstitutionEFConfiguration());

        modelBuilder.ApplyConfiguration<InstitutionClassification>(new InstitutionClassificationEFConfiguration());
        modelBuilder.ApplyConfiguration<InstitutionKind>(new InstitutionKindEFConfiguration());
        modelBuilder.ApplyConfiguration<InstitutionType>(new InstitutionTypeEFConfiguration());
        modelBuilder.ApplyConfiguration<InstitutionTypeSnapshot>(new InstitutionTypeSnapshotEFConfiguration());

        modelBuilder.ApplyConfiguration<InstitutionStatus>(new InstitutionStatusEFConfiguration());
        modelBuilder.ApplyConfiguration<InstitutionStatusSnapshot>(new InstitutionStatusSnapshotEFConfiguration());

        modelBuilder.ApplyConfiguration<InstitutionNameSnapshot>(new InstitutionNameSnapshotEFConfiguration());


        base.OnModelCreating(modelBuilder);
    }
}