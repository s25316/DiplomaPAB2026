using Diploma.Database.Models.Persons.PersonOperations;
using Diploma.Database.Models.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.Database.MsSql.Configurations.Shared;

public class EmailMessageEFConfiguration : IEntityTypeConfiguration<EmailMessage>
{
    public void Configure(EntityTypeBuilder<EmailMessage> builder)
    {
        builder.ToTable(nameof(EmailMessage));
        builder
            .HasKey(k => k.EmailMessageId)
            .HasName($"{nameof(EmailMessage)}_PK");

        builder
            .Property(k => k.EmailMessageId)
            .HasDefaultValueSql(DefaultValue.GUID);


        builder
            .HasOne(k => k.PersonOperation)
            .WithMany(k => k.EmailMessages)
            .HasForeignKey(k => k.PersonOperationId)
            .HasConstraintName($"{nameof(EmailMessage)}_{nameof(PersonOperation)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}