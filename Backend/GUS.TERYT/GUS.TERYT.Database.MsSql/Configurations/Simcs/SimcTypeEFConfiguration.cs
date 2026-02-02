// Ignore Spelling: Simc, Rodzaj
using GUS.TERYT.Database.Models.Simcs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.TERYT.Database.MsSql.Configurations.Simcs;

public class SimcTypeEFConfiguration : IEntityTypeConfiguration<SimcType>
{
    public void Configure(EntityTypeBuilder<SimcType> builder)
    {
        builder.ToTable(nameof(SimcType));
        builder
            .HasKey(k => k.TypeCode)
            .HasName($"{nameof(SimcType)}_PK");
        builder
            .Property(p => p.TypeCode)
            .HasMaxLength(DefaultValue.LENGTH_10);
        builder
            .Property(p => p.Name)
            .HasMaxLength(DefaultValue.LENGTH_100);


        builder
            .HasMany(k => k.Miejscowosci)
            .WithOne(k => k.Type)
            .HasForeignKey(k => k.TypeCode)
            .HasConstraintName($"{nameof(Simc)}_{nameof(SimcType)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
