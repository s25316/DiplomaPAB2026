using Diploma.Database.Models.Educations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.Database.MsSql.Configurations.Educations;

public class EducationCourseEFConfiguration : IEntityTypeConfiguration<EducationCourse>
{
    public void Configure(EntityTypeBuilder<EducationCourse> builder)
    {
        builder.ToTable(nameof(EducationCourse));
        builder
            .HasKey(k => k.EducationCourseId)
            .HasName($"{nameof(EducationCourse)}_PK");


        builder
            .HasMany(k => k.EducationCourseInstances)
            .WithOne(k => k.EducationCourse)
            .HasForeignKey(k => k.EducationCourseId)
            .HasConstraintName($"{nameof(EducationCourse)}_{nameof(EducationCourseInstance)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}