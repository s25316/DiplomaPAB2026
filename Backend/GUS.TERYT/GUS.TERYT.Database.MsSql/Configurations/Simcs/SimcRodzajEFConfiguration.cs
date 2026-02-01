// Ignore Spelling: Simc, Rodzaj
using GUS.TERYT.Database.Models.Simcs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.TERYT.Database.MsSql.Configurations.Simcs;

public class SimcRodzajEFConfiguration : IEntityTypeConfiguration<SimcRodzaj>
{
    public void Configure(EntityTypeBuilder<SimcRodzaj> builder)
    {
        builder.ToTable(nameof(SimcRodzaj));
        builder
            .HasKey(k => k.RodzajCode)
            .HasName($"{nameof(SimcRodzaj)}_PK");
        builder
            .Property(p => p.RodzajCode)
            .HasMaxLength(DefaultValue.LENGTH_10);
        builder
            .Property(p => p.Name)
            .HasMaxLength(DefaultValue.LENGTH_100);


        builder
            .HasMany(k => k.Miejscowosci)
            .WithOne(k => k.Rodzaj)
            .HasForeignKey(k => k.RodzajCode)
            .HasConstraintName($"{nameof(Simc)}_{nameof(SimcRodzaj)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
