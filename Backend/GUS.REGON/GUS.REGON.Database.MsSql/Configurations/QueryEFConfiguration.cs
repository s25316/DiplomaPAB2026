using GUS.REGON.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.REGON.Database.MsSql.Configurations;

public class QueryEFConfiguration : IEntityTypeConfiguration<Query>
{
    public void Configure(EntityTypeBuilder<Query> builder)
    {
        builder.ToTable(nameof(Query));
        builder
            .HasKey(k => k.Regon)
            .HasName($"{nameof(Query)}_PK");
    }
}