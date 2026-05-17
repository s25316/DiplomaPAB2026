using GUS.REGON.Database.Models;
using GUS.REGON.Database.Models.Pkds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.REGON.Database.MsSql.Configurations.Pkds;

public class InstitutionPkdEFConfiguration : IEntityTypeConfiguration<InstitutionPkd>
{
    public void Configure(EntityTypeBuilder<InstitutionPkd> builder)
    {
        builder.ToTable(nameof(InstitutionPkd));
        builder
            .HasKey(k => k.InstitutionPkdId)
            .HasName($"{nameof(InstitutionPkd)}_PK");

        builder
            .Property(p => p.InstitutionPkdId)
            .HasDefaultValueSql(DefaultValue.GUID);


        builder
            .HasOne(k => k.Institution)
            .WithMany(k => k.Pkds)
            .HasForeignKey(k => k.Regon)
            .HasConstraintName($"{nameof(InstitutionPkd)}_{nameof(Institution)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.Pkd)
            .WithMany(k => k.Institutions)
            .HasForeignKey(k => k.PkdCode)
            .HasConstraintName($"{nameof(InstitutionPkd)}_{nameof(Pkd)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}