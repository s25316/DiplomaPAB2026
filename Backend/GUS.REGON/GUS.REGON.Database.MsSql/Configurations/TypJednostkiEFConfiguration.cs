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
            .HasKey(k => k.TypJednostkiId)
            .HasName($"{nameof(TypJednostki)}_PK");
        builder
            .Property(p => p.Name)
            .HasMaxLength(int.MaxValue);


        builder
            .HasMany(k => k.Reports)
            .WithOne(k => k.TypJednostki)
            .HasForeignKey(k => k.TypJednostkiId)
            .HasConstraintName($"{nameof(Report)}_{nameof(TypJednostki)}_FK")
            .OnDelete(DeleteBehavior.Restrict);

        var data = new List<TypJednostki>()
        {
            new() {
                TypJednostkiId = "F",
                Name = "Jednostka Fizyczna",
            },
            new() {
                TypJednostkiId = "P",
                Name = "Jednostka Prawna",
            },
            new() {
                TypJednostkiId = "LF",
                Name = "Jednostka lokalna Jednostki Fizycznej",
            },
            new() {
                TypJednostkiId = "LP",
                Name = "Jednostka lokalna Jednostki Prawnej",
            },
        };
        builder.HasData(data);
    }
}