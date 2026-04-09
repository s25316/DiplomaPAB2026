using GUS.REGON.Database.Models;
using GUS.REGON.Database.Models.RegistrationDetails;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.REGON.Database.MsSql.Configurations.RegistrationDetails;

public class RodzajRejestruEFConfiguration : IEntityTypeConfiguration<RodzajRejestru>
{
    public void Configure(EntityTypeBuilder<RodzajRejestru> builder)
    {
        builder.ToTable(nameof(RodzajRejestru));
        builder
            .HasKey(k => k.Code)
            .HasName($"{nameof(RodzajRejestru)}_PK");
        builder
            .Property(p => p.Name)
            .HasMaxLength(int.MaxValue);


        builder
            .HasMany(k => k.Reports)
            .WithOne(k => k.RodzajRejestru)
            .HasForeignKey(k => k.RodzajRejestruCode)
            .HasConstraintName($"{nameof(Report)}_{nameof(RodzajRejestru)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}