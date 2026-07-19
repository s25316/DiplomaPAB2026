using Diploma.Database.Models.Educations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.Database.MsSql.Configurations.Educations;

public class EducationCourseInstanceEFConfiguration : IEntityTypeConfiguration<EducationCourseInstance>
{
    public void Configure(EntityTypeBuilder<EducationCourseInstance> builder)
    {
        builder.ToTable(nameof(EducationCourseInstance));
        builder
            .HasKey(k => k.EducationCourseInstanceId)
            .HasName($"{nameof(EducationCourseInstance)}_PK");
    }
}