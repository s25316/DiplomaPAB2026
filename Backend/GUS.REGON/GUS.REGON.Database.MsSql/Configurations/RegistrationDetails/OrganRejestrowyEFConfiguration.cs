using GUS.REGON.Database.Models;
using GUS.REGON.Database.Models.RegistrationDetails;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.REGON.Database.MsSql.Configurations.RegistrationDetails;

public class OrganRejestrowyEFConfiguration : IEntityTypeConfiguration<OrganRejestrowy>
{
    public void Configure(EntityTypeBuilder<OrganRejestrowy> builder)
    {
        builder.ToTable(nameof(OrganRejestrowy));
        builder
            .HasKey(k => k.OrganRejestrowyId)
            .HasName($"{nameof(OrganRejestrowy)}_PK");
        builder
            .Property(p => p.Name)
            .HasMaxLength(int.MaxValue);


        builder
            .HasMany(k => k.Reports)
            .WithOne(k => k.OrganRejestrowy)
            .HasForeignKey(k => k.OrganRejestrowyId)
            .HasConstraintName($"{nameof(Report)}_{nameof(OrganRejestrowy)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}