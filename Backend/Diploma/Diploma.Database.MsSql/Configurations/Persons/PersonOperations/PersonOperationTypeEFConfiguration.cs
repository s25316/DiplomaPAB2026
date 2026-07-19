using Diploma.Database.Models.Persons.PersonOperations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedPersonOperation = Diploma.Shared.PersonOperations.PersonOperation;

namespace Diploma.Database.MsSql.Configurations.Persons.PersonOperations;

public class PersonOperationTypeEFConfiguration : IEntityTypeConfiguration<PersonOperationType>
{
    public void Configure(EntityTypeBuilder<PersonOperationType> builder)
    {
        builder.ToTable(nameof(PersonOperationType));
        builder
            .HasKey(k => k.PersonOperationTypeId)
            .HasName($"{nameof(PersonOperationType)}_PK");

        builder
            .Property(p => p.PersonOperationTypeId)
            .ValueGeneratedNever();


        builder
            .HasMany(k => k.PersonOperations)
            .WithOne(k => k.PersonOperationType)
            .HasForeignKey(k => k.PersonOperationTypeId)
            .HasConstraintName($"{nameof(PersonOperation)}_{nameof(PersonOperationType)}_FK")
            .OnDelete(DeleteBehavior.Restrict);


        var data = SharedPersonOperation.All.Select(i => new PersonOperationType
        {
            PersonOperationTypeId = i.Id,
            Name = i.Name,
        });
        builder.HasData(data);
    }
}