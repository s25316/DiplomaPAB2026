using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RADON.Database.Models.Institutions;

namespace RADON.Database.MsSql.Configurations.Institutions;

internal class InstitutionTypeSnapshotEFConfiguration : IEntityTypeConfiguration<InstitutionTypeSnapshot>
{
    public void Configure(EntityTypeBuilder<InstitutionTypeSnapshot> builder)
    {
        builder.ToTable(nameof(InstitutionTypeSnapshot));
        builder
            .HasKey(k => k.InstitutionTypeSnapshotId)
            .HasName($"{nameof(InstitutionTypeSnapshot)}_PK");

        builder
            .Property(p => p.InstitutionTypeSnapshotId)
            .HasDefaultValueSql(DefaultValue.GUID);


        builder
            .HasOne(k => k.Institution)
            .WithMany(k => k.TypeSnapshots)
            .HasForeignKey(k => k.InstitutionUuid)
            .HasConstraintName($"{nameof(InstitutionTypeSnapshot)}_{nameof(Institution)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.InstitutionType)
            .WithMany(k => k.InstitutionSnapshots)
            .HasForeignKey(k => k.InstitutionTypeId)
            .HasConstraintName($"{nameof(InstitutionTypeSnapshot)}_{nameof(InstitutionType)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}