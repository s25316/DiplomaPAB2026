using GUS.REGON.Database.Models;
using GUS.REGON.Database.Models.RegistrationDetails;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.REGON.Database.MsSql.Configurations.RegistrationDetails;

public class OrganZalozycielskiEFConfiguration : IEntityTypeConfiguration<OrganZalozycielski>
{
    public void Configure(EntityTypeBuilder<OrganZalozycielski> builder)
    {
        builder.ToTable(nameof(OrganZalozycielski));
        builder
            .HasKey(k => k.OrganZalozycielskiId)
            .HasName($"{nameof(OrganZalozycielski)}_PK");
        builder
            .Property(p => p.Name)
            .HasMaxLength(int.MaxValue);


        builder
            .HasMany(k => k.Reports)
            .WithOne(k => k.OrganZalozycielski)
            .HasForeignKey(k => k.OrganZalozycielskiId)
            .HasConstraintName($"{nameof(Report)}_{nameof(OrganZalozycielski)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}