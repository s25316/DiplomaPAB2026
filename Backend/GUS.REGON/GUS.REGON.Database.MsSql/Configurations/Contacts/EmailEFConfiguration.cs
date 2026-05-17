using GUS.REGON.Database.Models;
using GUS.REGON.Database.Models.Contacts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.REGON.Database.MsSql.Configurations.Contacts;

public class EmailEFConfiguration : IEntityTypeConfiguration<Email>
{
    public void Configure(EntityTypeBuilder<Email> builder)
    {
        builder.ToTable(nameof(Email));
        builder
            .HasKey(k => k.EmailId)
            .HasName($"{nameof(Email)}_PK");
        builder
            .Property(p => p.EmailId)
            .HasDefaultValueSql(DefaultValue.GUID);
        builder
            .Property(p => p.Value)
            .HasMaxLength(int.MaxValue);


        var tableName = $"{nameof(Institution)}{nameof(Email)}";
        builder
            .HasMany(k => k.Institutions)
            .WithMany(k => k.Emails)
            .UsingEntity<Dictionary<string, object>>(
                tableName,
                l => l
                .HasOne<Institution>()
                .WithMany()
                .HasForeignKey($"{nameof(Institution.Regon)}")
                .HasConstraintName($"{tableName}_{nameof(Institution)}_FK")
                .OnDelete(DeleteBehavior.Cascade),
                r => r
                .HasOne<Email>()
                .WithMany()
                .HasForeignKey($"{nameof(Email.EmailId)}")
                .HasConstraintName($"{tableName}_{nameof(Email)}_FK")
                .OnDelete(DeleteBehavior.Cascade));
    }
}