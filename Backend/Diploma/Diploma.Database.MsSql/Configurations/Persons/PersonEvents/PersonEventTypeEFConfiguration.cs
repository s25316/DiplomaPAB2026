using Diploma.Database.Models.Persons.PersonEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedPersonEvent = Diploma.Shared.PersonEvents.PersonEvent;

namespace Diploma.Database.MsSql.Configurations.Persons.PersonEvents;

public class PersonEventTypeEFConfiguration : IEntityTypeConfiguration<PersonEventType>
{
    public void Configure(EntityTypeBuilder<PersonEventType> builder)
    {
        builder.ToTable(nameof(PersonEventType));
        builder
            .HasKey(k => k.PersonEventTypeId)
            .HasName($"{nameof(PersonEventType)}_PK");

        builder
            .Property(p => p.PersonEventTypeId)
            .ValueGeneratedNever();


        builder
            .HasMany(k => k.PersonEvents)
            .WithOne(k => k.PersonEventType)
            .HasForeignKey(k => k.PersonEventTypeId)
            .HasConstraintName($"{nameof(PersonEvent)}_{nameof(PersonEventType)}_FK")
            .OnDelete(DeleteBehavior.Restrict);


        var data = SharedPersonEvent.All.Select(i => new PersonEventType
        {
            PersonEventTypeId = i.Id,
            Name = i.Name,
        });
        builder.HasData(data);
    }
}