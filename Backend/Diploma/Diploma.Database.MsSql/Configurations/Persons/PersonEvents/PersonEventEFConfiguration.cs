using Diploma.Database.Models.Persons;
using Diploma.Database.Models.Persons.PersonEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.Database.MsSql.Configurations.Persons.PersonEvents;

public class PersonEventEFConfiguration : IEntityTypeConfiguration<PersonEvent>
{
    public void Configure(EntityTypeBuilder<PersonEvent> builder)
    {
        builder.ToTable(nameof(PersonEvent));
        builder
            .HasKey(k => k.PersonEventId)
            .HasName($"{nameof(PersonEvent)}_PK");

        builder
            .Property(p => p.PersonEventId)
            .HasDefaultValueSql(DefaultValue.GUID);


        builder
            .HasOne(k => k.Person)
            .WithMany(k => k.PersonEvents)
            .HasForeignKey(k => k.PersonId)
            .HasConstraintName($"{nameof(PersonEvent)}_{nameof(Person)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}