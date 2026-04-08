using GUS.REGON.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.REGON.Database.MsSql.Configurations;

public class ReportEFConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.ToTable(nameof(Report));
        builder
            .HasKey(k => k.Regon)
            .HasName($"{nameof(Report)}_PK");
        builder
            .Property(p => p.Nazwa)
            .HasMaxLength(int.MaxValue);
        builder
            .Property(p => p.NazwaSkrocona)
            .HasMaxLength(int.MaxValue);


        builder
            .HasOne(k => k.Query)
            .WithOne(k => k.Report)
            .HasForeignKey<Report>(k => k.Regon)
            .HasConstraintName($"{nameof(Query)}_{nameof(Report)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}