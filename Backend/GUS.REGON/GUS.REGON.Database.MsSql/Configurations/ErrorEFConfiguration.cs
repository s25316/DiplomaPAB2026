using GUS.REGON.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.REGON.Database.MsSql.Configurations;

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