using GUS.REGON.Database.Models;
using GUS.REGON.Database.Models.RegistrationDetails;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.REGON.Database.MsSql.Configurations.RegistrationDetails;

public class SzczegolnaFormaPrawnaEFConfiguration : IEntityTypeConfiguration<SzczegolnaFormaPrawna>
{
    public void Configure(EntityTypeBuilder<SzczegolnaFormaPrawna> builder)
    {
        builder.ToTable(nameof(SzczegolnaFormaPrawna));
        builder
            .HasKey(k => k.SzczegolnaFormaPrawnaCode)
            .HasName($"{nameof(SzczegolnaFormaPrawna)}_PK");
        builder
            .Property(p => p.Name)
            .HasMaxLength(int.MaxValue);


        builder
            .HasMany(k => k.Institutions)
            .WithOne(k => k.SzczegolnaFormaPrawna)
            .HasForeignKey(k => k.SzczegolnaFormaPrawnaCode)
            .HasConstraintName($"{nameof(Institution)}_{nameof(SzczegolnaFormaPrawna)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}