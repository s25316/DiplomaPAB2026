using GUS.REGON.Database.Models.Addresses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.REGON.Database.MsSql.Configurations.Addresses;

public class PowiatEFConfiguration : IEntityTypeConfiguration<Powiat>
{
    public void Configure(EntityTypeBuilder<Powiat> builder)
    {
        builder.ToTable(nameof(Powiat));
        builder
            .HasKey(k => k.PowiatId)
            .HasName($"{nameof(Powiat)}_PK");
        builder
            .Property(p => p.Name)
            .HasMaxLength(int.MaxValue);


        builder
            .HasMany(k => k.Addresses)
            .WithOne(k => k.Powiat)
            .HasForeignKey(k => k.PowiatId)
            .HasConstraintName($"{nameof(Address)}_{nameof(Powiat)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}