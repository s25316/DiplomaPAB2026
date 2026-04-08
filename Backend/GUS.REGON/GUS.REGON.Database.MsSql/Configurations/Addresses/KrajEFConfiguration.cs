using GUS.REGON.Database.Models.Addresses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.REGON.Database.MsSql.Configurations.Addresses;

public class KrajEFConfiguration : IEntityTypeConfiguration<Kraj>
{
    public void Configure(EntityTypeBuilder<Kraj> builder)
    {
        builder.ToTable(nameof(Kraj));
        builder
            .HasKey(k => k.Code)
            .HasName($"{nameof(Kraj)}_PK");
        builder
            .Property(p => p.Name)
            .HasMaxLength(int.MaxValue);


        builder
            .HasMany(k => k.Addresses)
            .WithOne(k => k.Kraj)
            .HasForeignKey(k => k.KrajCode)
            .HasConstraintName($"{nameof(Address)}_{nameof(Kraj)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}