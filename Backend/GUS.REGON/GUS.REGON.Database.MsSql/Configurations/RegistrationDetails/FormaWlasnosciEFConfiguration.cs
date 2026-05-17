using GUS.REGON.Database.Models;
using GUS.REGON.Database.Models.RegistrationDetails;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.REGON.Database.MsSql.Configurations.RegistrationDetails;

public class FormaWlasnosciEFConfiguration : IEntityTypeConfiguration<FormaWlasnosci>
{
    public void Configure(EntityTypeBuilder<FormaWlasnosci> builder)
    {
        builder.ToTable(nameof(FormaWlasnosci));
        builder
            .HasKey(k => k.FormaWlasnosciCode)
            .HasName($"{nameof(FormaWlasnosci)}_PK");
        builder
            .Property(p => p.Name)
            .HasMaxLength(int.MaxValue);


        builder
            .HasMany(k => k.Institutions)
            .WithOne(k => k.FormaWlasnosci)
            .HasForeignKey(k => k.FormaWlasnosciCode)
            .HasConstraintName($"{nameof(Institution)}_{nameof(FormaWlasnosci)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}