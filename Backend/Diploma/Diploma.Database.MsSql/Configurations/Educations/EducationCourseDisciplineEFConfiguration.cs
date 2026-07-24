using Diploma.Database.Models.Educations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.Database.MsSql.Configurations.Educations;

public class EducationCourseDisciplineEFConfiguration : IEntityTypeConfiguration<EducationCourseDiscipline>
{
    public void Configure(EntityTypeBuilder<EducationCourseDiscipline> builder)
    {
        builder.ToTable(nameof(EducationCourseDiscipline));
        builder
            .HasKey(k => k.EducationCourseDisciplineId)
            .HasName($"{nameof(EducationCourseDiscipline)}_PK");

        builder
            .Property(p => p.EducationCourseDisciplineId)
            .HasDefaultValueSql(DefaultValue.GUID);


        builder
            .HasOne(k => k.EducationCourse)
            .WithMany(k => k.EducationCourseDisciplines)
            .HasForeignKey(k => k.EducationCourseId)
            .HasConstraintName($"{typeof(EducationCourseDiscipline)}_{typeof(EducationCourse)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.EducationDiscipline)
            .WithMany(k => k.EducationCourseDisciplines)
            .HasForeignKey(k => k.EducationDisciplineCode)
            .HasConstraintName($"{typeof(EducationCourseDiscipline)}_{typeof(EducationDiscipline)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}