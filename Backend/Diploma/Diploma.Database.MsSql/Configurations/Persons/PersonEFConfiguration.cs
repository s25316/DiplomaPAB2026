using Diploma.Database.Models.Persons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.Database.MsSql.Configurations.Persons;

public class PersonEFConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable(nameof(Person));
        builder
            .HasKey(k => k.PersonId)
            .HasName($"{nameof(Person)}_PK");

        builder
            .Property(p => p.PersonId)
            .HasDefaultValueSql(DefaultValue.GUID);
    }
}