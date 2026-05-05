using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RADON.Database.Models.Institutions;
using RADON.Database.MsSql;

namespace RADON.Database.Models.Courses;

public class CourseEFConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable(nameof(Course));
        builder
            .HasKey(k => k.CourseUuid)
            .HasName($"{nameof(Course)}_PK");

        builder
            .Property(p => p.Name)
            .HasMaxLength(DefaultValue.LENGTH_100);


        builder
            .HasOne(k => k.MainInstitution)
            .WithMany(k => k.CoursesWithMainInstitution)
            .HasForeignKey(k => k.MainInstitutionUuid)
            .HasConstraintName($"{nameof(Institution)}_{nameof(Course)}_MAIN_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.LeadingInstitution)
            .WithMany(k => k.CoursesWithLeadingInstitution)
            .HasForeignKey(k => k.LeadingInstitutionUuid)
            .HasConstraintName($"{nameof(Institution)}_{nameof(Course)}_LEADING_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}