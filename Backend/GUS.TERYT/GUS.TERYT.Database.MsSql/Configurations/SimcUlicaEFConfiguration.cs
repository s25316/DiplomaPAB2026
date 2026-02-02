using GUS.TERYT.Database.Models;
using GUS.TERYT.Database.Models.Simcs;
using GUS.TERYT.Database.Models.Ulicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.TERYT.Database.MsSql.Configurations;

public class SimcUlicaEFConfiguration : IEntityTypeConfiguration<SimcUlica>
{
    public void Configure(EntityTypeBuilder<SimcUlica> builder)
    {
        builder.ToTable(nameof(SimcUlica));
        builder
            .HasKey(k => k.ConnectionId)
            .HasName($"{nameof(SimcUlica)}_PK");
        builder
            .HasAlternateKey(k => new { k.MiejscowoscCode, k.UlicaCode })
            .HasName($"{nameof(SimcUlica)}_AK");
        builder
            .Property(p => p.ConnectionId)
            .HasDefaultValueSql(DefaultValue.GUID);


        builder
            .HasOne(k => k.Miejscowosc)
            .WithMany(k => k.SimcUlicy)
            .HasForeignKey(k => k.MiejscowoscCode)
            .HasConstraintName($"{nameof(Simc)}_{nameof(SimcUlica)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.Ulica)
            .WithMany(k => k.SimcUlica)
            .HasForeignKey(k => k.UlicaCode)
            .HasConstraintName($"{nameof(Ulica)}_{nameof(SimcUlica)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
