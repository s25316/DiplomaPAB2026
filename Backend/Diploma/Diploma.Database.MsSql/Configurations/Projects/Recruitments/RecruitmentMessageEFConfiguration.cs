using Diploma.Database.Models.Persons;
using Diploma.Database.Models.Projects.Recruitments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.Database.MsSql.Configurations.Projects.Recruitments;

public class RecruitmentMessageEFConfiguration : IEntityTypeConfiguration<RecruitmentMessage>
{
    public void Configure(EntityTypeBuilder<RecruitmentMessage> builder)
    {
        builder.ToTable(nameof(RecruitmentMessage));
        builder
            .HasKey(k => k.RecruitmentMessageId)
            .HasName($"{nameof(RecruitmentMessage)}_PK");

        builder
            .Property(p => p.RecruitmentMessageId)
            .HasDefaultValueSql(DefaultValue.GUID);


        builder
            .HasOne(k => k.Recruitment)
            .WithMany(k => k.RecruitmentMessages)
            .HasForeignKey(k => k.RecruitmentId)
            .HasConstraintName($"{nameof(RecruitmentMessage)}_{nameof(Recruitment)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.Person)
            .WithMany(k => k.RecruitmentMessages)
            .HasForeignKey(k => k.PersonId)
            .HasConstraintName($"{nameof(RecruitmentMessage)}_{nameof(Person)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}