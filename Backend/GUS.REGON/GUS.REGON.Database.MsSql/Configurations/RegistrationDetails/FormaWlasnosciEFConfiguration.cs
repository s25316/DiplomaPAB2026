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
            .HasKey(k => k.FormaWlasnosciId)
            .HasName($"{nameof(FormaWlasnosci)}_PK");
        builder
            .Property(p => p.Name)
            .HasMaxLength(int.MaxValue);


        builder
            .HasMany(k => k.Reports)
            .WithOne(k => k.FormaWlasnosci)
            .HasForeignKey(k => k.FormaWlasnosciId)
            .HasConstraintName($"{nameof(Report)}_{nameof(FormaWlasnosci)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}