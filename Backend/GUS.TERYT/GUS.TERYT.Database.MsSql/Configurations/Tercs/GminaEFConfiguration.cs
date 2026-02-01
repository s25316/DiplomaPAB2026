// Ignore Spelling: Gmina
using GUS.TERYT.Database.Models.Simcs;
using GUS.TERYT.Database.Models.Tercs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.TERYT.Database.MsSql.Configurations.Tercs;

public class GminaEFConfiguration : IEntityTypeConfiguration<Gmina>
{
    public void Configure(EntityTypeBuilder<Gmina> builder)
    {
        builder.ToTable(nameof(Gmina));
        builder
            .HasKey(k => k.GminaId)
            .HasName($"{nameof(Gmina)}_PK");
        builder
            .HasAlternateKey(k => new { k.PowiatId, k.GminaCode, k.GminaRodzCode })
            .HasName($"{nameof(Gmina)}_AK");
        builder
            .Property(p => p.GminaId)
            .HasDefaultValueSql(DefaultValue.GUID);
        builder
            .Property(p => p.GminaCode)
            .HasMaxLength(DefaultValue.LENGTH_10);
        builder
            .Property(p => p.Name)
            .HasMaxLength(DefaultValue.LENGTH_100);


        builder
            .HasMany(k => k.Miejscowosci)
            .WithOne(k => k.Gmina)
            .HasForeignKey(k => k.GminaId)
            .HasConstraintName($"{nameof(Gmina)}_{nameof(Simc)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
