// Ignore Spelling: Ulic
using GUS.TERYT.Database.Models.Ulics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.TERYT.Database.MsSql.Configurations.Ulics;

public class UlicEFConfiguration : IEntityTypeConfiguration<Ulic>
{
    public void Configure(EntityTypeBuilder<Ulic> builder)
    {
        builder.ToTable(nameof(Ulic));
        builder
            .HasKey(k => k.UlicId)
            .HasName($"{nameof(Ulic)}_PK");
        builder
            .Property(k => k.UlicId)
            .HasMaxLength(DefaultValue.LENGTH_10);
        builder
            .Property(k => k.Name)
            .HasMaxLength(DefaultValue.LENGTH_100);
    }
}
