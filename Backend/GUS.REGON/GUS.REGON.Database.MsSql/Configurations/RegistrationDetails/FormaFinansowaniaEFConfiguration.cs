using GUS.REGON.Database.Models;
using GUS.REGON.Database.Models.RegistrationDetails;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.REGON.Database.MsSql.Configurations.RegistrationDetails;

public class FormaFinansowaniaEFConfiguration : IEntityTypeConfiguration<FormaFinansowania>
{
    public void Configure(EntityTypeBuilder<FormaFinansowania> builder)
    {
        builder.ToTable(nameof(FormaFinansowania));
        builder
            .HasKey(k => k.Code)
            .HasName($"{nameof(FormaFinansowania)}_PK");
        builder
            .Property(p => p.Name)
            .HasMaxLength(int.MaxValue);


        builder
            .HasMany(k => k.Reports)
            .WithOne(k => k.FormaFinansowania)
            .HasForeignKey(k => k.FormaFinansowaniaCode)
            .HasConstraintName($"{nameof(Report)}_{nameof(FormaFinansowania)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}