using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RADON.Database.Enums;
using RADON.Database.Models.Institutions;

namespace RADON.Database.MsSql.Configurations.Institutions;

internal class InstitutionClassificationEFConfiguration : IEntityTypeConfiguration<InstitutionClassification>
{
    public void Configure(EntityTypeBuilder<InstitutionClassification> builder)
    {
        builder.ToTable(nameof(InstitutionClassification));
        builder
            .HasKey(k => k.InstitutionClassificationCode)
            .HasName($"{nameof(InstitutionClassification)}_PK");

        builder
            .Property(p => p.InstitutionClassificationCode)
            .HasMaxLength(DefaultValue.LENGTH_100);
        builder
            .Property(p => p.Name)
            .HasMaxLength(DefaultValue.LENGTH_100);


        builder
            .HasMany(k => k.Kinds)
            .WithOne(k => k.Classification)
            .HasForeignKey(k => k.ClassificationCode)
            .HasConstraintName($"{nameof(InstitutionClassification)}_{nameof(InstitutionKind)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasMany(k => k.Types)
            .WithOne(k => k.Classification)
            .HasForeignKey(k => k.ClassificationCode)
            .HasConstraintName($"{nameof(InstitutionClassification)}_{nameof(InstitutionType)}_FK")
            .OnDelete(DeleteBehavior.Restrict);

        var data = new List<InstitutionClassification>()
        {
            new (){ InstitutionClassificationCode = ((int)InstitutionClassificationCode.UNIVERSITY).ToString(), Name = "UCZELNIA" },
            new (){ InstitutionClassificationCode = ((int)InstitutionClassificationCode.SCIENTIFIC_INSTITUTION).ToString(), Name = "INSTYTUCJA NAUKOWA" },
            new (){ InstitutionClassificationCode = ((int)InstitutionClassificationCode.FEDERATION).ToString(), Name = "FEDERACJA" },
        };

        builder.HasData(data);
    }
}