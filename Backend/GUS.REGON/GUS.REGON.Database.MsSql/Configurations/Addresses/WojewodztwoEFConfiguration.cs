using GUS.REGON.Database.Models.Addresses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.REGON.Database.MsSql.Configurations.Addresses;

internal class WojewodztwoEFConfiguration : IEntityTypeConfiguration<Wojewodztwo>
{
    public void Configure(EntityTypeBuilder<Wojewodztwo> builder)
    {
        builder.ToTable(nameof(Wojewodztwo));
        builder
            .HasKey(k => k.Code)
            .HasName($"{nameof(Wojewodztwo)}_PK");
        builder
            .Property(p => p.Name)
            .HasMaxLength(int.MaxValue);


        builder
            .HasMany(k => k.Addresses)
            .WithOne(k => k.Wojewodztwo)
            .HasForeignKey(k => k.WojewodztwoCode)
            .HasConstraintName($"{nameof(Address)}_{nameof(Wojewodztwo)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}