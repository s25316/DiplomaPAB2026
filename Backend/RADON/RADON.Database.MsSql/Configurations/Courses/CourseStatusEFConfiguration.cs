using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RADON.Database.MsSql;

namespace RADON.Database.Models.Courses;

public class CourseStatusEFConfiguration : IEntityTypeConfiguration<CourseStatus>
{
    public void Configure(EntityTypeBuilder<CourseStatus> builder)
    {
        builder.ToTable(nameof(CourseStatus));
        builder
            .HasKey(k => k.CourseStatusCode)
            .HasName($"{nameof(CourseStatus)}_PK");

        builder
            .Property(p => p.CourseStatusCode)
            .HasMaxLength(DefaultValue.LENGTH_50);
        builder
            .Property(p => p.Name)
            .HasMaxLength(DefaultValue.LENGTH_450);


        builder
            .HasMany(k => k.Courses)
            .WithOne(k => k.CourseStatus)
            .HasForeignKey(k => k.CourseStatusCode)
            .HasConstraintName($"{nameof(Course)}_{nameof(CourseStatus)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}