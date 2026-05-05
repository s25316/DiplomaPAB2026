using Microsoft.EntityFrameworkCore;
using RADON.Database.Models;
using RADON.Database.Models.Courses;
using RADON.Database.Models.Institutions;
using RADON.Database.Models.Shared;

namespace RADON.Database;

public class RadonDbContext(DbContextOptions options) : DbContext(options)
{
    public virtual DbSet<DataSource> DataSources { get; set; }

    #region INSTITUTIONS
    public virtual DbSet<Institution> Institutions { get; set; }

    public virtual DbSet<InstitutionClassification> InstitutionClassifications { get; set; }
    public virtual DbSet<InstitutionKind> InstitutionKinds { get; set; }
    public virtual DbSet<InstitutionType> InstitutionTypes { get; set; }
    public virtual DbSet<InstitutionTypeSnapshot> InstitutionTypeSnapshots { get; set; }

    public virtual DbSet<InstitutionStatus> InstitutionStatuses { get; set; }
    public virtual DbSet<InstitutionStatusSnapshot> InstitutionStatusSnapshots { get; set; }

    public virtual DbSet<InstitutionNameSnapshot> InstitutionNameSnapshots { get; set; }
    #endregion


    #region COURSES
    public virtual DbSet<Course> Courses { get; set; }
    public virtual DbSet<CourseInstance> CourseInstances { get; set; }

    public virtual DbSet<CourseForm> CourseForms { get; set; }
    public virtual DbSet<CourseInstanceStatus> CourseInstanceStatuses { get; set; }
    public virtual DbSet<CourseLevel> CourseLevels { get; set; }
    public virtual DbSet<CourseProfile> CourseProfiles { get; set; }
    public virtual DbSet<CourseStatus> CourseStatuses { get; set; }
    public virtual DbSet<Isced> Isceds { get; set; }
    public virtual DbSet<Language> Languages { get; set; }
    public virtual DbSet<ProfessionalTitle> ProfessionalTitles { get; set; }
    #endregion


    #region SHARED
    public virtual DbSet<CourseDiscipline> CourseDisciplines { get; set; }
    public virtual DbSet<Discipline> Disciplines { get; set; }
    #endregion
}