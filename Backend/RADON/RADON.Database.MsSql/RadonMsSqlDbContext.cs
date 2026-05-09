using Microsoft.EntityFrameworkCore;
using RADON.Database.Models;
using RADON.Database.Models.Courses;
using RADON.Database.Models.Institutions;
using RADON.Database.Models.Shared;
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
        modelBuilder.ApplyConfiguration<Error>(new ErrorEFConfiguration());
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


        // --- COURSES ---
        modelBuilder.ApplyConfiguration<Course>(new CourseEFConfiguration());
        modelBuilder.ApplyConfiguration<CourseInstance>(new CourseInstanceEFConfiguration());

        modelBuilder.ApplyConfiguration<CourseForm>(new CourseFormEFConfiguration());
        modelBuilder.ApplyConfiguration<CourseInstanceStatus>(new CourseInstanceStatusEFConfiguration());
        modelBuilder.ApplyConfiguration<CourseLevel>(new CourseLevelEFConfiguration());
        modelBuilder.ApplyConfiguration<CourseProfile>(new CourseProfileEFConfiguration());
        modelBuilder.ApplyConfiguration<CourseStatus>(new CourseStatusEFConfiguration());
        modelBuilder.ApplyConfiguration<Isced>(new IscedEFConfiguration());
        modelBuilder.ApplyConfiguration<Language>(new LanguageEFConfiguration());
        modelBuilder.ApplyConfiguration<ProfessionalTitle>(new ProfessionalTitleEFConfiguration());


        // --- SHARED ---
        modelBuilder.ApplyConfiguration<CourseDiscipline>(new CourseDisciplineEFConfiguration());
        modelBuilder.ApplyConfiguration<Discipline>(new DisciplineEFConfiguration());


        base.OnModelCreating(modelBuilder);
    }
}