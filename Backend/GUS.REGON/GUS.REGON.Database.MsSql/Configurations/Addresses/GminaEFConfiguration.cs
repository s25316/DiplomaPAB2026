using GUS.REGON.Database.Models.Addresses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.REGON.Database.MsSql.Configurations.Addresses;

internal class GminaEFConfiguration : IEntityTypeConfiguration<Gmina>
{
    public void Configure(EntityTypeBuilder<Gmina> builder)
    {
        builder.ToTable(nameof(Gmina));
        builder
            .HasKey(k => k.Code)
            .HasName($"{nameof(Gmina)}_PK");
        builder
            .Property(p => p.Name)
            .HasMaxLength(int.MaxValue);


        builder
            .HasMany(k => k.Addresses)
            .WithOne(k => k.Gmina)
            .HasForeignKey(k => k.GminaCode)
            .HasConstraintName($"{nameof(Address)}_{nameof(Gmina)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}