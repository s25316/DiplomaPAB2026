using GUS.REGON.Database.Models.Pkds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.REGON.Database.MsSql.Configurations.Pkds;

public class PkdEFConfiguration : IEntityTypeConfiguration<Pkd>
{
    public void Configure(EntityTypeBuilder<Pkd> builder)
    {
        builder.ToTable(nameof(Pkd));
        builder
            .HasKey(k => k.PkdCode)
            .HasName($"{nameof(Pkd)}_PK");
    }
}