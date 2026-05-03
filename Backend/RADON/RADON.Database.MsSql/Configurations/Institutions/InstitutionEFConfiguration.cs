using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RADON.Database.Models.Institutions;

namespace RADON.Database.MsSql.Configurations.Institutions;

internal class InstitutionEFConfiguration : IEntityTypeConfiguration<Institution>
{
    public void Configure(EntityTypeBuilder<Institution> builder)
    {
        builder.ToTable(nameof(Institution));
        builder
            .HasKey(k => k.InstitutionUuid)
            .HasName($"{nameof(Institution)}_PK");

        builder
            .Property(p => p.Regon)
            .HasMaxLength(DefaultValue.LENGTH_100);
        builder
            .Property(p => p.Nip)
            .HasMaxLength(DefaultValue.LENGTH_100);
        builder
            .Property(p => p.Krs)
            .HasMaxLength(DefaultValue.LENGTH_100);

        builder
            .Property(p => p.Www)
            .HasMaxLength(DefaultValue.LENGTH_100);
        builder
            .Property(p => p.Email)
            .HasMaxLength(DefaultValue.LENGTH_100);
        builder
            .Property(p => p.Phone)
            .HasMaxLength(DefaultValue.LENGTH_100);
    }
}