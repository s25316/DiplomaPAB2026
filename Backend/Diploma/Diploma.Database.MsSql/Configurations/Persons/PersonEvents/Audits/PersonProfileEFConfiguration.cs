using Diploma.Database.Models.Persons.PersonEvents;
using Diploma.Database.Models.Persons.PersonEvents.Audits;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.Database.MsSql.Configurations.Persons.PersonEvents.Audits;

public class PersonProfileEFConfiguration : IEntityTypeConfiguration<PersonProfile>
{
    public void Configure(EntityTypeBuilder<PersonProfile> builder)
    {
        builder.ToTable(nameof(PersonProfile));
        builder
            .HasKey(k => k.PersonProfileId)
            .HasName($"{nameof(PersonProfile)}_PK");

        builder
            .Property(p => p.PersonProfileId)
            .HasDefaultValueSql(DefaultValue.GUID);


        builder
            .HasOne(k => k.PersonEvent)
            .WithOne(k => k.PersonProfile)
            .HasForeignKey<PersonProfile>(k => k.PersonEventId)
            .HasConstraintName($"{nameof(PersonEvent)}_{nameof(PersonProfile)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}