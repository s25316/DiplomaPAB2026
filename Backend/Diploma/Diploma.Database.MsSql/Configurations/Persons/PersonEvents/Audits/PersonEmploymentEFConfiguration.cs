using Diploma.Database.Models.Persons.PersonEvents;
using Diploma.Database.Models.Persons.PersonEvents.Audits;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.Database.MsSql.Configurations.Persons.PersonEvents.Audits;

public class PersonEmploymentEFConfiguration : IEntityTypeConfiguration<PersonEmployment>
{
    public void Configure(EntityTypeBuilder<PersonEmployment> builder)
    {
        builder.ToTable(nameof(PersonEmployment));
        builder
            .HasKey(k => k.PersonEmploymentId)
            .HasName($"{nameof(PersonEmployment)}_PK");

        builder
            .Property(p => p.PersonEmploymentId)
            .HasDefaultValueSql(DefaultValue.GUID);


        builder
            .HasOne(k => k.PersonEvent)
            .WithOne(k => k.PersonEmployment)
            .HasForeignKey<PersonEmployment>(k => k.PersonEventId)
            .HasConstraintName($"{nameof(PersonEvent)}_{nameof(PersonEmployment)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.Root)
            .WithMany(k => k.History)
            .HasForeignKey(k => k.RootId)
            .HasConstraintName($"{nameof(PersonEmployment)}_{nameof(PersonEmployment)}_ROOT_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.Next)
            .WithOne(k => k.Previous)
            .HasForeignKey<PersonEmployment>(k => k.NextId)
            .HasConstraintName($"{nameof(PersonEmployment)}_{nameof(PersonEmployment)}_NEXT_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}