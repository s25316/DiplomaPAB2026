// Ignore Spelling: Powiat
using GUS.TERYT.Database.Models.Tercs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.TERYT.Database.MsSql.Configurations.Tercs;

public class PowiatEFConfiguration : IEntityTypeConfiguration<Powiat>
{
    public void Configure(EntityTypeBuilder<Powiat> builder)
    {
        builder.ToTable(nameof(Powiat));
        builder
            .HasKey(k => k.PowiatId)
            .HasName($"{nameof(Powiat)}_PK");
        builder
            .HasAlternateKey(k => new { k.WojewodztwoCode, k.PowiatCode })
            .HasName($"{nameof(Powiat)}_AK");
        builder
            .Property(p => p.PowiatId)
            .HasDefaultValueSql(DefaultValue.GUID);
        builder
            .Property(p => p.PowiatCode)
            .HasMaxLength(DefaultValue.LENGTH_10);
        builder
            .Property(p => p.Name)
            .HasMaxLength(DefaultValue.LENGTH_100);


        builder
            .HasMany(k => k.Gminy)
            .WithOne(k => k.Powiat)
            .HasForeignKey(k => k.PowiatId)
            .HasConstraintName($"{nameof(Powiat)}_{nameof(Gmina)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
