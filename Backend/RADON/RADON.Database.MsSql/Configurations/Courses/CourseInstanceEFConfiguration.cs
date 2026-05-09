using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RADON.Database.MsSql;

namespace RADON.Database.Models.Courses;

public class CourseInstanceEFConfiguration : IEntityTypeConfiguration<CourseInstance>
{
    public void Configure(EntityTypeBuilder<CourseInstance> builder)
    {
        builder.ToTable(nameof(CourseInstance));
        builder
            .HasKey(k => k.CourseInstanceUuid)
            .HasName($"{nameof(CourseInstance)}_PK");

        builder
            .Property(p => p.Name)
            .HasMaxLength(DefaultValue.LENGTH_450);


        builder
            .HasOne(k => k.Course)
            .WithMany(k => k.CourseInstances)
            .HasForeignKey(k => k.CourseUuid)
            .HasConstraintName($"{nameof(Course)}_{nameof(CourseInstance)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}