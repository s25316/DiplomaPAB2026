using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RADON.Database.MsSql;

namespace RADON.Database.Models.Courses;

public class ProfessionalTitleEFConfiguration : IEntityTypeConfiguration<ProfessionalTitle>
{
    public string ProfessionalTitleCode { get; set; } = null!;
    public string Name { get; set; } = null!;


    public virtual ICollection<CourseInstanceEFConfiguration> CourseInstances { get; set; } = [];

    public void Configure(EntityTypeBuilder<ProfessionalTitle> builder)
    {
        builder.ToTable(nameof(ProfessionalTitle));
        builder
            .HasKey(k => k.ProfessionalTitleCode)
            .HasName($"{nameof(ProfessionalTitle)}_PK");

        builder
            .Property(p => p.ProfessionalTitleCode)
            .HasMaxLength(DefaultValue.LENGTH_100);
        builder
            .Property(p => p.Name)
            .HasMaxLength(DefaultValue.LENGTH_100);


        builder
            .HasMany(k => k.CourseInstances)
            .WithOne(k => k.ProfessionalTitle)
            .HasForeignKey(k => k.ProfessionalTitleCode)
            .HasConstraintName($"{nameof(CourseInstance)}_{nameof(ProfessionalTitle)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}