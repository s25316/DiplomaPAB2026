using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RADON.Database.Models.Institutions;

namespace RADON.Database.MsSql.Configurations.Institutions;

internal class InstitutionStatusEFConfiguration : IEntityTypeConfiguration<InstitutionStatus>
{
    public void Configure(EntityTypeBuilder<InstitutionStatus> builder)
    {
        builder.ToTable(nameof(InstitutionStatus));
        builder
            .HasKey(k => k.InstitutionStatusCode)
            .HasName($"{nameof(InstitutionStatus)}_PK");

        builder
            .Property(p => p.InstitutionStatusCode)
            .HasMaxLength(DefaultValue.LENGTH_100);
        builder
            .Property(p => p.Name)
            .HasMaxLength(DefaultValue.LENGTH_100);
    }
}