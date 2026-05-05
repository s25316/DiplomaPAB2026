using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RADON.Database.Models.Courses;
using RADON.Database.MsSql;

namespace RADON.Database.Models.Shared;

public class CourseDisciplineEFConfiguration : IEntityTypeConfiguration<CourseDiscipline>
{
    public void Configure(EntityTypeBuilder<CourseDiscipline> builder)
    {
        builder.ToTable(nameof(CourseDiscipline));
        builder
            .HasKey(k => k.CourseDisciplineUuid)
            .HasName($"{nameof(CourseDiscipline)}_PK");

        builder
            .Property(p => p.CourseDisciplineUuid)
            .HasDefaultValueSql(DefaultValue.GUID);


        builder
            .HasOne(k => k.Course)
            .WithMany(k => k.Disciplines)
            .HasForeignKey(k => k.CourseUuid)
            .HasConstraintName($"{nameof(CourseDiscipline)}_{nameof(Course)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.Discipline)
            .WithMany(k => k.Courses)
            .HasForeignKey(k => k.DisciplineCode)
            .HasConstraintName($"{nameof(CourseDiscipline)}_{nameof(Discipline)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}