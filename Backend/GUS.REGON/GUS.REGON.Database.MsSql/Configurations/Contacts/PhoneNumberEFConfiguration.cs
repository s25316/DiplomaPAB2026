using GUS.REGON.Database.Models;
using GUS.REGON.Database.Models.Contacts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.REGON.Database.MsSql.Configurations.Contacts;

public class PhoneNumberEFConfiguration : IEntityTypeConfiguration<PhoneNumber>
{
    public void Configure(EntityTypeBuilder<PhoneNumber> builder)
    {
        builder.ToTable(nameof(PhoneNumber));
        builder
            .HasKey(k => k.PhoneNumberId)
            .HasName($"{nameof(PhoneNumber)}_PK");
        builder
            .Property(p => p.PhoneNumberId)
            .HasDefaultValueSql(DefaultValue.GUID);
        builder
            .Property(p => p.Value)
            .HasMaxLength(int.MaxValue);


        var tableName = $"{nameof(Report)}{nameof(PhoneNumber)}";
        builder
            .HasMany(k => k.Reports)
            .WithMany(k => k.PhoneNumbers)
            .UsingEntity<Dictionary<string, object>>(
                tableName,
                l => l
                .HasOne<Report>()
                .WithMany()
                .HasForeignKey($"{nameof(Report.Regon)}")
                .HasConstraintName($"{tableName}_{nameof(Report)}_FK")
                .OnDelete(DeleteBehavior.Cascade),
                r => r
                .HasOne<PhoneNumber>()
                .WithMany()
                .HasForeignKey($"{nameof(PhoneNumber.PhoneNumberId)}")
                .HasConstraintName($"{tableName}_{nameof(PhoneNumber)}_FK")
                .OnDelete(DeleteBehavior.Cascade));
    }
}