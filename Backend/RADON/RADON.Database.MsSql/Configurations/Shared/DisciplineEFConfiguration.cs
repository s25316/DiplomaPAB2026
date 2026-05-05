using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RADON.Database.MsSql;

namespace RADON.Database.Models.Shared;

public class DisciplineEFConfiguration : IEntityTypeConfiguration<Discipline>
{
    public void Configure(EntityTypeBuilder<Discipline> builder)
    {
        builder.ToTable(nameof(Discipline));
        builder
            .HasKey(k => k.DisciplineCode)
            .HasName($"{nameof(Discipline)}_PK");

        builder
            .Property(p => p.DisciplineCode)
            .HasMaxLength(DefaultValue.LENGTH_100);
        builder
            .Property(p => p.Name)
            .HasMaxLength(DefaultValue.LENGTH_100);
    }
}