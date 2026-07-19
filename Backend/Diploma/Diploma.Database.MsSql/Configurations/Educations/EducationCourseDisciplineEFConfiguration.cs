using Diploma.Database.Models.Educations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.Database.MsSql.Configurations.Educations;

public class EducationCourseDisciplineEFConfiguration : IEntityTypeConfiguration<EducationCourseDiscipline>
{
    public void Configure(EntityTypeBuilder<EducationCourseDiscipline> builder)
    {
        builder.ToTable(nameof(EducationCourseDiscipline));
        builder
            .HasKey(k => k.Code)
            .HasName($"{nameof(EducationCourseDiscipline)}_PK");
    }
}