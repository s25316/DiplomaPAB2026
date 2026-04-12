using GUS.REGON.Database.Models;
using GUS.REGON.Database.Models.Addresses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.REGON.Database.MsSql.Configurations.Addresses;

public class AddressEFConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable(nameof(Address));
        builder
            .HasKey(k => k.AddressId)
            .HasName($"{nameof(Address)}_PK");
        builder
            .Property(p => p.AddressId)
            .HasDefaultValueSql(DefaultValue.GUID);
        builder
            .Property(p => p.KodPocztowy)
            .HasMaxLength(int.MaxValue);
        builder
            .Property(p => p.NumerNieruchomosci)
            .HasMaxLength(int.MaxValue);
        builder
            .Property(p => p.NumerLokalu)
            .HasMaxLength(int.MaxValue);
        builder
            .Property(p => p.NietypoweMiejsceLokalizacji)
            .HasMaxLength(int.MaxValue);


        builder
            .HasMany(k => k.Reports)
            .WithOne(k => k.Address)
            .HasForeignKey(k => k.AddressId)
            .HasConstraintName($"{nameof(Report)}_{nameof(Address)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}