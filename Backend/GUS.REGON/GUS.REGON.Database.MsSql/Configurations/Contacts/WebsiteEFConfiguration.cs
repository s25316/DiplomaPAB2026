using GUS.REGON.Database.Models;
using GUS.REGON.Database.Models.Contacts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.REGON.Database.MsSql.Configurations.Contacts;

public class WebsiteEFConfiguration : IEntityTypeConfiguration<Website>
{
    public void Configure(EntityTypeBuilder<Website> builder)
    {
        builder.ToTable(nameof(Website));
        builder
            .HasKey(k => k.WebsiteId)
            .HasName($"{nameof(Website)}_PK");
        builder
            .Property(p => p.WebsiteId)
            .HasDefaultValueSql(DefaultValue.GUID);
        builder
            .Property(p => p.Value)
            .HasMaxLength(int.MaxValue);


        var tableName = $"{nameof(Report)}{nameof(Website)}";
        builder
            .HasMany(k => k.Reports)
            .WithMany(k => k.Websites)
            .UsingEntity<Dictionary<string, object>>(
                tableName,
                l => l
                .HasOne<Report>()
                .WithMany()
                .HasForeignKey($"{nameof(Report.Regon)}")
                .HasConstraintName($"{tableName}_{nameof(Report)}_FK")
                .OnDelete(DeleteBehavior.Cascade),
                r => r
                .HasOne<Website>()
                .WithMany()
                .HasForeignKey($"{nameof(Website.WebsiteId)}")
                .HasConstraintName($"{tableName}_{nameof(Website)}_FK")
                .OnDelete(DeleteBehavior.Cascade));
    }
}