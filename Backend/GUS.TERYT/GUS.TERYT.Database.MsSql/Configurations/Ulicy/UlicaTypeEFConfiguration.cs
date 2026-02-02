// Ignore Spelling: Ulica, Ulicy
using GUS.TERYT.Database.Models.Ulicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.TERYT.Database.MsSql.Configurations.Ulicy;

public class UlicaTypeEFConfiguration : IEntityTypeConfiguration<UlicaType>
{
    public void Configure(EntityTypeBuilder<UlicaType> builder)
    {
        builder.ToTable(nameof(UlicaType));
        builder
            .HasKey(k => k.TypeCode)
            .HasName($"{nameof(UlicaType)}_PK");
        builder
            .Property(k => k.TypeCode)
            .ValueGeneratedNever();
        builder
            .Property(k => k.Name)
            .HasMaxLength(DefaultValue.LENGTH_100);


        builder
            .HasMany(k => k.Ulicy)
            .WithOne(k => k.Type)
            .HasForeignKey(k => k.TypeCode)
            .HasConstraintName($"{nameof(Ulica)}_{nameof(UlicaType)}_PK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
