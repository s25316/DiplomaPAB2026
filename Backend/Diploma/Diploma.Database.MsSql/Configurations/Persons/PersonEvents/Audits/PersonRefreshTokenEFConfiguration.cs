using Diploma.Database.Models.Persons.PersonEvents;
using Diploma.Database.Models.Persons.PersonEvents.Audits;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.Database.MsSql.Configurations.Persons.PersonEvents.Audits;

public class PersonRefreshTokenEFConfiguration : IEntityTypeConfiguration<PersonRefreshToken>
{
    public void Configure(EntityTypeBuilder<PersonRefreshToken> builder)
    {
        builder.ToTable(nameof(PersonRefreshToken));
        builder
            .HasKey(k => k.PersonRefreshTokenId)
            .HasName($"{nameof(PersonRefreshToken)}_PK");

        builder
            .Property(p => p.PersonRefreshTokenId)
            .HasDefaultValueSql(DefaultValue.GUID);


        builder
            .HasOne(k => k.LoginInEvent)
            .WithOne(k => k.PersonLoginIn)
            .HasForeignKey<PersonRefreshToken>(k => k.LoginInEventId)
            .HasConstraintName($"{nameof(PersonEvent)}_{nameof(PersonProfile)}_LOGIN_IN_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.LogOutEvent)
            .WithOne(k => k.PersonLogOut)
            .HasForeignKey<PersonRefreshToken>(k => k.LogOutEventId)
            .HasConstraintName($"{nameof(PersonEvent)}_{nameof(PersonProfile)}_LOG_OUT_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}