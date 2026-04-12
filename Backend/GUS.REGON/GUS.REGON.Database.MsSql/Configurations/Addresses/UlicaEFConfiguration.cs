using GUS.REGON.Database.Models.Addresses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.REGON.Database.MsSql.Configurations.Addresses;

public class UlicaEFConfiguration : IEntityTypeConfiguration<Ulica>
{
    public void Configure(EntityTypeBuilder<Ulica> builder)
    {
        builder.ToTable(nameof(Ulica));
        builder
            .HasKey(k => k.UlicaId)
            .HasName($"{nameof(Ulica)}_PK");
        builder
            .Property(p => p.Name)
            .HasMaxLength(int.MaxValue);


        builder
            .HasMany(k => k.Addresses)
            .WithOne(k => k.Ulica)
            .HasForeignKey(k => k.UlicaId)
            .HasConstraintName($"{nameof(Address)}_{nameof(Ulica)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}