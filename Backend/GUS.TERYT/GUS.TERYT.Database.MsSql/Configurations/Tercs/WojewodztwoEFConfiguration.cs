// Ignore Spelling: Tercs, Wojewodztwo
using GUS.TERYT.Database.Models.Tercs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.TERYT.Database.MsSql.Configurations.Tercs;

public class WojewodztwoEFConfiguration : IEntityTypeConfiguration<Wojewodztwo>
{
    public void Configure(EntityTypeBuilder<Wojewodztwo> builder)
    {
        builder.ToTable(nameof(Wojewodztwo));
        builder
            .HasKey(k => k.WojewodztwoCode)
            .HasName($"{nameof(Wojewodztwo)}_PK");
        builder
            .Property(p => p.WojewodztwoCode)
            .HasMaxLength(DefaultValue.LENGTH_10);
        builder
            .Property(p => p.Name)
            .HasMaxLength(DefaultValue.LENGTH_100);


        builder
            .HasMany(k => k.Powiaty)
            .WithOne(k => k.Wojewodztwo)
            .HasForeignKey(k => k.WojewodztwoCode)
            .HasConstraintName($"{nameof(Wojewodztwo)}_{nameof(Powiat)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}