using Diploma.Database.Models.Persons;
using Diploma.Database.Models.Persons.PersonOperations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.Database.MsSql.Configurations.Persons.PersonOperations;

public class PersonOperationEFConfiguration : IEntityTypeConfiguration<PersonOperation>
{
    public void Configure(EntityTypeBuilder<PersonOperation> builder)
    {
        builder.ToTable(nameof(PersonOperation));
        builder
            .HasKey(k => k.PersonOperationId)
            .HasName($"{nameof(PersonOperation)}_PK");

        builder
            .Property(p => p.PersonOperationId)
            .HasDefaultValueSql(DefaultValue.GUID);


        builder
            .HasOne(k => k.Person)
            .WithMany(p => p.PersonOperations)
            .HasForeignKey(k => k.PersonId)
            .HasConstraintName($"{nameof(PersonOperation)}_{nameof(Person)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}