// Ignore Spelling: Ulica, Ulicy
using GUS.TERYT.Database.Models.Ulicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.TERYT.Database.MsSql.Configurations.Ulicy;

public class UlicaEFConfiguration : IEntityTypeConfiguration<Ulica>
{
    public void Configure(EntityTypeBuilder<Ulica> builder)
    {
        builder.ToTable(nameof(Ulica));
        builder
            .HasKey(k => k.UlicaCode)
            .HasName($"{nameof(Ulica)}_PK");
        builder
            .Property(k => k.UlicaCode)
            .HasMaxLength(DefaultValue.LENGTH_10);
        builder
            .Property(k => k.Name)
            .HasMaxLength(DefaultValue.LENGTH_100);
    }
}
