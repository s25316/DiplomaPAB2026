// Ignore Spelling: Wojewodstwo
using GUS.TERYT.Database.Models.Tercs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.TERYT.Database.MsSql.Configurations.Tercs;

public class WojewodstwoEFConfiguration : IEntityTypeConfiguration<Wojewodstwo>
{
    public void Configure(EntityTypeBuilder<Wojewodstwo> builder)
    {
        builder.ToTable(nameof(Wojewodstwo));
        builder
            .HasKey(k => k.WojewodstwoId)
            .HasName($"{nameof(Wojewodstwo)}_PK");
        builder
            .Property(p => p.WojewodstwoId)
            .HasMaxLength(DefaultValue.LENGTH_10);
        builder
            .Property(p => p.Name)
            .HasMaxLength(DefaultValue.LENGTH_100);


        builder
            .HasMany(k => k.Powiaty)
            .WithOne(k => k.Wojewodstwo)
            .HasForeignKey(k => k.WojewodstwoId)
            .HasConstraintName($"{nameof(Wojewodstwo)}_{nameof(Powiat)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
