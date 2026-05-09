using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RADON.Database.MsSql;

namespace RADON.Database.Models.Courses;

public class CourseLevelEFConfiguration : IEntityTypeConfiguration<CourseLevel>
{
    public void Configure(EntityTypeBuilder<CourseLevel> builder)
    {
        builder.ToTable(nameof(CourseLevel));
        builder
            .HasKey(k => k.CourseLevelCode)
            .HasName($"{nameof(CourseLevel)}_PK");

        builder
            .Property(p => p.CourseLevelCode)
            .HasMaxLength(DefaultValue.LENGTH_50);
        builder
            .Property(p => p.Name)
            .HasMaxLength(DefaultValue.LENGTH_450);


        builder
            .HasMany(k => k.Courses)
            .WithOne(k => k.CourseLevel)
            .HasForeignKey(k => k.CourseLevelCode)
            .HasConstraintName($"{nameof(Course)}_{nameof(CourseLevel)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}