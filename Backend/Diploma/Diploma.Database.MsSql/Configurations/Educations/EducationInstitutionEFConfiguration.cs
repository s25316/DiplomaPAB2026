using Diploma.Database.Models.Educations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.Database.MsSql.Configurations.Educations;

public class EducationInstitutionEFConfiguration : IEntityTypeConfiguration<EducationInstitution>
{
    public void Configure(EntityTypeBuilder<EducationInstitution> builder)
    {
        builder.ToTable(nameof(EducationInstitution));
        builder
            .HasKey(k => k.EducationInstitutionId)
            .HasName($"{nameof(EducationInstitution)}_PK");


        builder
            .HasMany(k => k.EducationCourses)
            .WithOne(k => k.EducationInstitution)
            .HasForeignKey(k => k.EducationInstitutionId)
            .HasConstraintName($"{nameof(EducationInstitution)}_{nameof(EducationCourse)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}