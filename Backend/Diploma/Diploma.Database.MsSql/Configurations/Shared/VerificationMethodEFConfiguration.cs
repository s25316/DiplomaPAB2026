using Diploma.Database.Models.Persons.PersonOperations;
using Diploma.Database.Models.Shared;
using Diploma.Shared.Verifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.Database.MsSql.Configurations.Shared;

public class VerificationMethodEFConfiguration : IEntityTypeConfiguration<VerificationMethod>
{
    public void Configure(EntityTypeBuilder<VerificationMethod> builder)
    {
        builder.ToTable(nameof(VerificationMethod));
        builder
            .HasKey(k => k.VerificationMethodId)
            .HasName($"{nameof(VerificationMethod)}_PK");

        builder
            .Property(p => p.VerificationMethodId)
            .ValueGeneratedNever();


        builder
            .HasMany(k => k.PersonOperations)
            .WithOne(k => k.VerificationMethod)
            .HasForeignKey(k => k.VerificationMethodId)
            .HasConstraintName($"{nameof(PersonOperation)}_{nameof(VerificationMethod)}_FK")
            .OnDelete(DeleteBehavior.Restrict);


        var data = Verification.All.Select(i => new VerificationMethod()
        {
            VerificationMethodId = i.Id,
            Name = i.Name
        });
        builder.HasData(data);
    }
}