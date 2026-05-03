using Microsoft.EntityFrameworkCore;
using RADON.Database.Models;
using RADON.Database.Models.Institutions;

namespace RADON.Database;

public class RadonDbContext(DbContextOptions options) : DbContext(options)
{
    public virtual DbSet<DataSource> DataSources { get; set; }


    public virtual DbSet<Institution> Institutions { get; set; }

    public virtual DbSet<InstitutionClassification> InstitutionClassifications { get; set; }
    public virtual DbSet<InstitutionKind> InstitutionKinds { get; set; }
    public virtual DbSet<InstitutionType> InstitutionTypes { get; set; }
    public virtual DbSet<InstitutionTypeSnapshot> InstitutionTypeSnapshots { get; set; }

    public virtual DbSet<InstitutionStatus> InstitutionStatuses { get; set; }
    public virtual DbSet<InstitutionStatusSnapshot> InstitutionStatusSnapshots { get; set; }

    public virtual DbSet<InstitutionNameSnapshot> InstitutionNameSnapshots { get; set; }
}