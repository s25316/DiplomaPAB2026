using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RADON.Database.MsSql;

namespace RADON.Database.Models.Courses;

public class CourseFormEFConfiguration : IEntityTypeConfiguration<CourseForm>
{
    public void Configure(EntityTypeBuilder<CourseForm> builder)
    {
        builder.ToTable(nameof(CourseForm));
        builder
            .HasKey(k => k.CourseFormCode)
            .HasName($"{nameof(CourseForm)}_PK");

        builder
            .Property(p => p.CourseFormCode)
            .HasMaxLength(DefaultValue.LENGTH_100);
        builder
            .Property(p => p.Name)
            .HasMaxLength(DefaultValue.LENGTH_100);


        builder
            .HasMany(k => k.CourseInstances)
            .WithOne(k => k.CourseForm)
            .HasForeignKey(k => k.CourseFormCode)
            .HasConstraintName($"{nameof(CourseInstance)}_{nameof(CourseForm)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}