using Diploma.Database.Models.Persons;
using Diploma.Database.Models.Projects.Recruitments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.Database.MsSql.Configurations.Projects.Recruitments;

public class RecruitmentStatusAuditEFConfiguration : IEntityTypeConfiguration<RecruitmentStatusAudit>
{
    public void Configure(EntityTypeBuilder<RecruitmentStatusAudit> builder)
    {
        builder.ToTable(nameof(RecruitmentStatusAudit));
        builder
            .HasKey(k => k.RecruitmentStatusAuditId)
            .HasName($"{nameof(RecruitmentStatusAudit)}_PK");

        builder
            .Property(p => p.RecruitmentStatusAuditId)
            .HasDefaultValueSql(DefaultValue.GUID);

        builder
           .HasOne(k => k.Root)
           .WithMany(k => k.History)
           .HasForeignKey(k => k.RootId)
           .HasConstraintName($"{nameof(RecruitmentStatusAudit)}_{nameof(RecruitmentStatusAudit)}_ROOT_FK")
           .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.Next)
            .WithOne(k => k.Previous)
            .HasForeignKey<RecruitmentStatusAudit>(k => k.NextId)
            .HasConstraintName($"{nameof(RecruitmentStatusAudit)}_{nameof(RecruitmentStatusAudit)}_NEXT_FK")
            .OnDelete(DeleteBehavior.Restrict);


        builder
            .HasOne(p => p.Recruitment)
            .WithMany(k => k.RecruitmentStatusAudits)
            .HasForeignKey(k => k.RecruitmentId)
            .HasConstraintName($"{nameof(RecruitmentStatusAudit)}_{nameof(Recruitment)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(p => p.RecruitmentStatus)
            .WithMany(k => k.RecruitmentStatusAudits)
            .HasForeignKey(k => k.RecruitmentStatusId)
            .HasConstraintName($"{nameof(RecruitmentStatusAudit)}_{nameof(RecruitmentStatus)}_FK")
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(p => p.Recruitments)
            .WithOne(k => k.LastRecruitmentStatusAudit)
            .HasForeignKey(k => k.LastRecruitmentStatusAuditId)
            .HasConstraintName($"{nameof(Recruitment)}_{nameof(RecruitmentStatusAudit)}_FK")
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(p => p.Person)
            .WithMany(k => k.RecruitmentStatusAudits)
            .HasForeignKey(k => k.PersonId)
            .HasConstraintName($"{nameof(Person)}_{nameof(RecruitmentStatusAudit)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
