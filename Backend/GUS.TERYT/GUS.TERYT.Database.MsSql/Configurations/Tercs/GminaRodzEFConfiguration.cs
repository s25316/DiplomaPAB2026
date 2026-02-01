// Ignore Spelling: Gmina, Rodz
using GUS.TERYT.Database.Models.Tercs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.TERYT.Database.MsSql.Configurations.Tercs;

public class GminaRodzEFConfiguration : IEntityTypeConfiguration<GminaRodz>
{
    public void Configure(EntityTypeBuilder<GminaRodz> builder)
    {
        builder.ToTable(nameof(GminaRodz));
        builder
            .HasKey(k => k.GminaRodzCode)
            .HasName($"{nameof(GminaRodz)}_PK");
        builder
            .Property(p => p.GminaRodzCode)
            .HasMaxLength(DefaultValue.LENGTH_10);
        builder
            .Property(p => p.Name)
            .HasMaxLength(DefaultValue.LENGTH_100);


        builder
            .HasMany(k => k.Gminy)
            .WithOne(k => k.Rodz)
            .HasForeignKey(k => k.GminaRodzCode)
            .HasConstraintName($"{nameof(Gmina)}_{nameof(GminaRodz)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
