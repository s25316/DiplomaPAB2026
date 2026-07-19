using Diploma.Database.Models.Persons.PersonEvents;
using Diploma.Database.Models.Persons.PersonEvents.Audits;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.Database.MsSql.Configurations.Persons.PersonEvents.Audits;

public class PersonUriEFConfiguration : IEntityTypeConfiguration<PersonUri>
{
    public void Configure(EntityTypeBuilder<PersonUri> builder)
    {
        builder.ToTable(nameof(PersonUri));
        builder
            .HasKey(k => k.PersonUriId)
            .HasName($"{nameof(PersonUri)}_PK");

        builder
            .Property(p => p.PersonUriId)
            .HasDefaultValueSql(DefaultValue.GUID);


        builder
            .HasOne(k => k.PersonEvent)
            .WithOne(k => k.PersonUri)
            .HasForeignKey<PersonUri>(k => k.PersonEventId)
            .HasConstraintName($"{nameof(PersonEvent)}_{nameof(PersonUri)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.Root)
            .WithMany(k => k.History)
            .HasForeignKey(k => k.RootId)
            .HasConstraintName($"{nameof(PersonUri)}_{nameof(PersonUri)}_ROOT_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.Next)
            .WithOne(k => k.Previous)
            .HasForeignKey<PersonUri>(k => k.NextId)
            .HasConstraintName($"{nameof(PersonUri)}_{nameof(PersonUri)}_NEXT_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
