using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RADON.Database.Models.Institutions;

namespace RADON.Database.MsSql.Configurations.Institutions;

internal class InstitutionNameSnapshotEFConfiguration : IEntityTypeConfiguration<InstitutionNameSnapshot>
{
    public void Configure(EntityTypeBuilder<InstitutionNameSnapshot> builder)
    {
        builder.ToTable(nameof(InstitutionNameSnapshot));
        builder
            .HasKey(k => k.InstitutionNameSnapshotId)
            .HasName($"{nameof(InstitutionNameSnapshot)}_PK");

        builder
            .Property(p => p.InstitutionNameSnapshotId)
            .HasDefaultValueSql(DefaultValue.GUID);
        builder
            .Property(p => p.Name)
            .HasMaxLength(DefaultValue.LENGTH_450);


        builder
            .HasOne(k => k.Institution)
            .WithMany(k => k.NameSnapshots)
            .HasForeignKey(k => k.InstitutionUuid)
            .HasConstraintName($"{nameof(Institution)}_{nameof(InstitutionNameSnapshot)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}