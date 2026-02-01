using GUS.TERYT.Database.Models;
using GUS.TERYT.Database.Models.Simcs;
using GUS.TERYT.Database.Models.Ulics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.TERYT.Database.MsSql.Configurations;

public class SimcUlicEFConfiguration : IEntityTypeConfiguration<SimcUlic>
{
    public void Configure(EntityTypeBuilder<SimcUlic> builder)
    {
        builder.ToTable(nameof(SimcUlic));
        builder
            .HasKey(k => k.ConnectionId)
            .HasName($"{nameof(SimcUlic)}_PK");
        builder
            .HasAlternateKey(k => new { k.MiejscowoscId, k.UlicId })
            .HasName($"{nameof(SimcUlic)}_AK");
        builder
            .Property(p => p.ConnectionId)
            .HasDefaultValueSql(DefaultValue.GUID);


        builder
            .HasOne(k => k.Miejscowosc)
            .WithMany(k => k.SimcUlics)
            .HasForeignKey(k => k.MiejscowoscId)
            .HasConstraintName($"{nameof(Simc)}_{nameof(SimcUlic)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.Ulic)
            .WithMany(k => k.SimcUlics)
            .HasForeignKey(k => k.UlicId)
            .HasConstraintName($"{nameof(Ulic)}_{nameof(SimcUlic)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
