using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RADON.Database.MsSql;

namespace RADON.Database.Models.Courses;

public class IscedEFConfiguration : IEntityTypeConfiguration<Isced>
{
    public void Configure(EntityTypeBuilder<Isced> builder)
    {
        builder.ToTable(nameof(Isced));
        builder
            .HasKey(k => k.IscedCode)
            .HasName($"{nameof(Isced)}_PK");

        builder
            .Property(p => p.IscedCode)
            .HasMaxLength(DefaultValue.LENGTH_50);
        builder
            .Property(p => p.Name)
            .HasMaxLength(DefaultValue.LENGTH_450);


        builder
            .HasMany(k => k.Courses)
            .WithOne(k => k.Isced)
            .HasForeignKey(k => k.IscedCode)
            .HasConstraintName($"{nameof(Course)}_{nameof(Isced)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}