// Ignore Spelling: Powiat
using GUS.TERYT.Database.Models.Tercs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.TERYT.Database.MsSql.Configurations.Tercs;

public class PowiatTypeEFConfiguration : IEntityTypeConfiguration<PowiatType>
{
    public void Configure(EntityTypeBuilder<PowiatType> builder)
    {
        builder.ToTable(nameof(PowiatType));
        builder
            .HasKey(k => k.TypeId)
            .HasName($"{nameof(PowiatType)}_PK");
        builder
            .Property(p => p.TypeId)
            .ValueGeneratedNever();
        builder
            .Property(p => p.Name)
            .HasMaxLength(DefaultValue.LENGTH_100);


        builder
            .HasMany(k => k.Powiaty)
            .WithOne(k => k.PowiatType)
            .HasForeignKey(k => k.TypeId)
            .HasConstraintName($"{nameof(Powiat)}_{nameof(PowiatType)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
