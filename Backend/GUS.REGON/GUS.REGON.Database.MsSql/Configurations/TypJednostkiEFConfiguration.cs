// Ignore Spelling: Jednostka, Jednostki, lokalna, Fizyczna, Fizycznej, Prawna, Prawnej
using GUS.REGON.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.REGON.Database.MsSql.Configurations;

public class TypJednostkiEFConfiguration : IEntityTypeConfiguration<TypJednostki>
{
    public void Configure(EntityTypeBuilder<TypJednostki> builder)
    {
        builder.ToTable(nameof(TypJednostki));
        builder
            .HasKey(k => k.TypJednostkiCode)
            .HasName($"{nameof(TypJednostki)}_PK");
        builder
            .Property(p => p.Name)
            .HasMaxLength(int.MaxValue);


        builder
            .HasMany(k => k.Institutions)
            .WithOne(k => k.TypJednostki)
            .HasForeignKey(k => k.TypJednostkiCode)
            .HasConstraintName($"{nameof(Institution)}_{nameof(TypJednostki)}_FK")
            .OnDelete(DeleteBehavior.Restrict);

        var data = new List<TypJednostki>()
        {
            new() {
                TypJednostkiCode = "F",
                Name = "Jednostka Fizyczna",
            },
            new() {
                TypJednostkiCode = "P",
                Name = "Jednostka Prawna",
            },
            new() {
                TypJednostkiCode = "LF",
                Name = "Jednostka lokalna Jednostki Fizycznej",
            },
            new() {
                TypJednostkiCode = "LP",
                Name = "Jednostka lokalna Jednostki Prawnej",
            },
        };
        builder.HasData(data);
    }
}