// Ignore Spelling: Simc
using GUS.TERYT.Database.Models.Simcs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.TERYT.Database.MsSql.Configurations.Simcs;

public class SimcEFConfiguration : IEntityTypeConfiguration<Simc>
{
    public void Configure(EntityTypeBuilder<Simc> builder)
    {
        builder.ToTable(nameof(Simc));
        builder
            .HasKey(k => k.MiejscowoscCode)
            .HasName($"{nameof(Simc)}_PK");
        builder
            .Property(p => p.MiejscowoscCode)
            .HasMaxLength(DefaultValue.LENGTH_10);
        builder
            .Property(p => p.Name)
            .HasMaxLength(DefaultValue.LENGTH_100);
    }
}
