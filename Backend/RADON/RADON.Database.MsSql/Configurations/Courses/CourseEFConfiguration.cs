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
            .HasMaxLength(DefaultValue.LENGTH_450);


        builder
            .HasOne(k => k.Institution)
            .WithMany(k => k.Courses)
            .HasForeignKey(k => k.InstitutionUuid)
            .HasConstraintName($"{nameof(Institution)}_{nameof(Course)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}