using GUS.REGON.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.REGON.Database.MsSql.Configurations;

public class InstitutionEFConfiguration : IEntityTypeConfiguration<Institution>
{
    public void Configure(EntityTypeBuilder<Institution> builder)
    {
        builder.ToTable(nameof(Institution));
        builder
            .HasKey(k => k.Regon)
            .HasName($"{nameof(Institution)}_PK");
        builder
            .Property(p => p.Nazwa)
            .HasMaxLength(int.MaxValue);
        builder
            .Property(p => p.NazwaSkrocona)
            .HasMaxLength(int.MaxValue);


        builder
            .HasOne(k => k.Request)
            .WithOne(k => k.Institution)
            .HasForeignKey<Institution>(k => k.Regon)
            .HasConstraintName($"{nameof(Request)}_{nameof(Institution)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}