using Diploma.Database.Models.Educations;
using Diploma.Shared.Semesters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.Database.MsSql.Configurations.Educations;

public class EducationSemesterEFConfiguration : IEntityTypeConfiguration<EducationSemester>
{
    public void Configure(EntityTypeBuilder<EducationSemester> builder)
    {
        builder.ToTable(nameof(EducationSemester));
        builder
            .HasKey(k => k.EducationSemesterId)
            .HasName($"{nameof(EducationSemester)}_PK");

        builder
            .Property(p => p.EducationSemesterId)
            .ValueGeneratedNever();


        var data = Semester.All.Select(i => new EducationSemester
        {
            EducationSemesterId = i.Id,
            Name = i.Name,
        });
        builder.HasData(data);
    }
}