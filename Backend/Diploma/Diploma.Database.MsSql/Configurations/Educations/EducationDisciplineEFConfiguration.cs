using Diploma.Database.Models.Educations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.Database.MsSql.Configurations.Educations;

public class EducationDisciplineEFConfiguration : IEntityTypeConfiguration<EducationDiscipline>
{
    public void Configure(EntityTypeBuilder<EducationDiscipline> builder)
    {
        builder.ToTable(nameof(EducationDiscipline));
        builder
            .HasKey(k => k.Code)
            .HasName($"{nameof(EducationDiscipline)}_PK");
    }
}