using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RADON.Database.Models;

namespace RADON.Database.MsSql.Configurations;

public class ErrorEFConfiguration : IEntityTypeConfiguration<Error>
{
    public void Configure(EntityTypeBuilder<Error> builder)
    {
        builder.ToTable(nameof(Error));
        builder.HasKey(k => k.ErrorId)
            .HasName($"{nameof(Error)}_PK");

        builder
            .Property(p => p.ErrorId)
            .HasDefaultValueSql(DefaultValue.GUID);
    }
}