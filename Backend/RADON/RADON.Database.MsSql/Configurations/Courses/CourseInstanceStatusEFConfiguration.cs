using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RADON.Database.MsSql;

namespace RADON.Database.Models.Courses;

public class CourseInstanceStatusEFConfiguration : IEntityTypeConfiguration<CourseInstanceStatus>
{
    public void Configure(EntityTypeBuilder<CourseInstanceStatus> builder)
    {
        builder.ToTable(nameof(CourseInstanceStatus));
        builder
            .HasKey(k => k.CourseInstanceStatusCode)
            .HasName($"{nameof(CourseInstanceStatus)}_PK");

        builder
            .Property(p => p.CourseInstanceStatusCode)
            .HasMaxLength(DefaultValue.LENGTH_50);
        builder
            .Property(p => p.Name)
            .HasMaxLength(DefaultValue.LENGTH_450);


        builder
            .HasMany(k => k.CourseInstances)
            .WithOne(k => k.CourseInstanceStatus)
            .HasForeignKey(k => k.CourseInstanceStatusCode)
            .HasConstraintName($"{nameof(CourseInstance)}_{nameof(CourseInstanceStatus)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}