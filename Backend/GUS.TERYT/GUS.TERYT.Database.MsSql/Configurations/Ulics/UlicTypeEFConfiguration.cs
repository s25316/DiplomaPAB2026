// Ignore Spelling: Ulic
using GUS.TERYT.Database.Models.Ulics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.TERYT.Database.MsSql.Configurations.Ulics;

public class UlicTypeEFConfiguration : IEntityTypeConfiguration<UlicType>
{
    public void Configure(EntityTypeBuilder<UlicType> builder)
    {
        builder.ToTable(nameof(UlicType));
        builder
            .HasKey(k => k.TypeId)
            .HasName($"{nameof(UlicType)}_PK");
        builder
            .Property(k => k.TypeId)
            .ValueGeneratedNever();
        builder
            .Property(k => k.Name)
            .HasMaxLength(DefaultValue.LENGTH_100);


        builder
            .HasMany(k => k.Ulicy)
            .WithOne(k => k.Type)
            .HasForeignKey(k => k.TypeId)
            .HasConstraintName($"{nameof(Ulic)}_{nameof(UlicType)}_PK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
