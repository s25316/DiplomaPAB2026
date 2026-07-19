using Diploma.Database.Models.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.Database.MsSql.Configurations.Shared;

internal class ErrorEFConfiguration : IEntityTypeConfiguration<Error>
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