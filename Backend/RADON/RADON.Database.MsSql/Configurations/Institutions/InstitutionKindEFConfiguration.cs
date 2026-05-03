using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RADON.Database.Models.Institutions;

namespace RADON.Database.MsSql.Configurations.Institutions;

internal class InstitutionKindEFConfiguration : IEntityTypeConfiguration<InstitutionKind>
{
    public void Configure(EntityTypeBuilder<InstitutionKind> builder)
    {
        builder.ToTable(nameof(InstitutionKind));
        builder
            .HasKey(k => k.InstitutionKindCode)
            .HasName($"{nameof(InstitutionKind)}_PK");

        builder
            .Property(p => p.InstitutionKindCode)
            .HasMaxLength(DefaultValue.LENGTH_100);
        builder
            .Property(p => p.Name)
            .HasMaxLength(DefaultValue.LENGTH_100);


        builder
            .HasMany(k => k.Institutions)
            .WithOne(k => k.InstitutionKind)
            .HasForeignKey(k => k.InstitutionKindCode)
            .HasConstraintName($"{nameof(Institution)}_{nameof(InstitutionKind)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}