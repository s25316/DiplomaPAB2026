using GUS.REGON.Database.Models.Addresses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.REGON.Database.MsSql.Configurations.Addresses;

public class MiejscowoscEFConfiguration : IEntityTypeConfiguration<Miejscowosc>
{
    public void Configure(EntityTypeBuilder<Miejscowosc> builder)
    {
        builder.ToTable(nameof(Miejscowosc));
        builder
            .HasKey(k => k.MiejscowoscId)
            .HasName($"{nameof(Miejscowosc)}_PK");
        builder
            .Property(p => p.Name)
            .HasMaxLength(int.MaxValue);


        builder
            .HasMany(k => k.Addresses)
            .WithOne(k => k.Miejscowosc)
            .HasForeignKey(k => k.MiejscowoscId)
            .HasConstraintName($"{nameof(Address)}_{nameof(Miejscowosc)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}