using GUS.REGON.Database.Models.Addresses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.REGON.Database.MsSql.Configurations.Addresses;

public class MiejscowoscPocztyEFConfiguration : IEntityTypeConfiguration<MiejscowoscPoczty>
{
    public void Configure(EntityTypeBuilder<MiejscowoscPoczty> builder)
    {
        builder.ToTable(nameof(MiejscowoscPoczty));
        builder
            .HasKey(k => k.Code)
            .HasName($"{nameof(MiejscowoscPoczty)}_PK");
        builder
            .Property(p => p.Name)
            .HasMaxLength(int.MaxValue);


        builder
            .HasMany(k => k.Addresses)
            .WithOne(k => k.MiejscowoscPoczty)
            .HasForeignKey(k => k.MiejscowoscPocztyCode)
            .HasConstraintName($"{nameof(Address)}_{nameof(MiejscowoscPoczty)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}