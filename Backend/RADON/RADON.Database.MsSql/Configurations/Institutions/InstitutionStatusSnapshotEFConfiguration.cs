using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RADON.Database.Models.Institutions;

namespace RADON.Database.MsSql.Configurations.Institutions;

internal class InstitutionStatusSnapshotEFConfiguration : IEntityTypeConfiguration<InstitutionStatusSnapshot>
{
    public void Configure(EntityTypeBuilder<InstitutionStatusSnapshot> builder)
    {
        builder.ToTable(nameof(InstitutionStatusSnapshot));
        builder
            .HasKey(k => k.InstitutionStatusSnapshotId)
            .HasName($"{nameof(InstitutionStatusSnapshot)}_PK");

        builder
            .Property(p => p.InstitutionStatusSnapshotId)
            .HasDefaultValueSql(DefaultValue.GUID);


        builder
            .HasOne(k => k.Institution)
            .WithMany(k => k.StatusSnapshots)
            .HasForeignKey(k => k.InstitutionUuid)
            .HasConstraintName($"{nameof(InstitutionStatusSnapshot)}_{nameof(Institution)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.InstitutionStatus)
            .WithMany(k => k.InstitutionSnapshots)
            .HasForeignKey(k => k.InstitutionStatusCode)
            .HasConstraintName($"{nameof(InstitutionStatusSnapshot)}_{nameof(InstitutionStatus)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}