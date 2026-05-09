using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RADON.Database.Models.Institutions;

namespace RADON.Database.MsSql.Configurations.Institutions;

internal class InstitutionTypeEFConfiguration : IEntityTypeConfiguration<InstitutionType>
{
    public void Configure(EntityTypeBuilder<InstitutionType> builder)
    {
        builder.ToTable(nameof(InstitutionType));
        builder
            .HasKey(k => k.InstitutionTypeId)
            .HasName($"{nameof(InstitutionType)}_PK");

        builder
            .Property(p => p.InstitutionTypeId)
            .HasDefaultValueSql(DefaultValue.GUID);
        builder
            .Property(p => p.InstitutionTypeCode)
            .HasMaxLength(DefaultValue.LENGTH_50);
        builder
            .Property(p => p.Name)
            .HasMaxLength(DefaultValue.LENGTH_450);
    }
}