using Diploma.Database.Models.Persons.PersonEvents;
using Diploma.Database.Models.Persons.PersonEvents.Audits;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.Database.MsSql.Configurations.Persons.PersonEvents.Audits;

public class PersonIdentityEFConfiguration : IEntityTypeConfiguration<PersonIdentity>
{
    public void Configure(EntityTypeBuilder<PersonIdentity> builder)
    {
        builder.ToTable(nameof(PersonIdentity));
        builder
            .HasKey(k => k.PersonIdentityId)
            .HasName($"{nameof(PersonIdentity)}_PK");

        builder
            .Property(p => p.PersonIdentityId)
            .HasDefaultValueSql(DefaultValue.GUID);


        builder
            .HasOne(k => k.PersonEvent)
            .WithOne(k => k.PersonIdentity)
            .HasForeignKey<PersonIdentity>(k => k.PersonEventId)
            .HasConstraintName($"{nameof(PersonEvent)}_{nameof(PersonIdentity)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}