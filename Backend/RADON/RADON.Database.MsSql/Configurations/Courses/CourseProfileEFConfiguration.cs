using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RADON.Database.MsSql;

namespace RADON.Database.Models.Courses;

public class CourseProfileEFConfiguration : IEntityTypeConfiguration<CourseProfile>
{
    public void Configure(EntityTypeBuilder<CourseProfile> builder)
    {
        builder.ToTable(nameof(CourseProfile));
        builder
            .HasKey(k => k.CourseProfileCode)
            .HasName($"{nameof(CourseProfile)}_PK");

        builder
            .Property(p => p.CourseProfileCode)
            .HasMaxLength(DefaultValue.LENGTH_100);
        builder
            .Property(p => p.Name)
            .HasMaxLength(DefaultValue.LENGTH_100);


        builder
            .HasMany(k => k.Courses)
            .WithOne(k => k.CourseProfile)
            .HasForeignKey(k => k.CourseProfileCode)
            .HasConstraintName($"{nameof(Course)}_{nameof(CourseProfile)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}