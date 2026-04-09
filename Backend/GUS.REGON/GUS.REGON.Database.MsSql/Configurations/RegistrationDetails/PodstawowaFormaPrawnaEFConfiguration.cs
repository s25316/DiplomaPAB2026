using GUS.REGON.Database.Models;
using GUS.REGON.Database.Models.RegistrationDetails;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.REGON.Database.MsSql.Configurations.RegistrationDetails;

public class PodstawowaFormaPrawnaEFConfiguration : IEntityTypeConfiguration<PodstawowaFormaPrawna>
{
    public void Configure(EntityTypeBuilder<PodstawowaFormaPrawna> builder)
    {
        builder.ToTable(nameof(PodstawowaFormaPrawna));
        builder
            .HasKey(k => k.Code)
            .HasName($"{nameof(PodstawowaFormaPrawna)}_PK");
        builder
            .Property(p => p.Name)
            .HasMaxLength(int.MaxValue);


        builder
            .HasMany(k => k.Reports)
            .WithOne(k => k.PodstawowaFormaPrawna)
            .HasForeignKey(k => k.PodstawowaFormaPrawnaCode)
            .HasConstraintName($"{nameof(Report)}_{nameof(PodstawowaFormaPrawna)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}