using GUS.REGON.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.REGON.Database.MsSql.Configurations;

public class RequestEFConfiguration : IEntityTypeConfiguration<Request>
{
    public void Configure(EntityTypeBuilder<Request> builder)
    {
        builder.ToTable(nameof(Request));
        builder
            .HasKey(k => k.Regon)
            .HasName($"{nameof(Request)}_PK");
    }
}