using Diploma.Database.Models.Projects.Recruitments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedRecruitmentStatus = Diploma.Shared.RecruitmentStatuses.RecruitmentStatus;

namespace Diploma.Database.MsSql.Configurations.Projects.Recruitments;

public class RecruitmentStatusEFConfiguration : IEntityTypeConfiguration<RecruitmentStatus>
{
    public void Configure(EntityTypeBuilder<RecruitmentStatus> builder)
    {
        builder.ToTable(nameof(RecruitmentStatus));
        builder
            .HasKey(k => k.RecruitmentStatusId)
            .HasName($"{nameof(RecruitmentStatus)}_PK");

        builder
            .Property(p => p.RecruitmentStatusId)
            .ValueGeneratedNever();


        builder
            .HasMany(p => p.Recruitments)
            .WithOne(k => k.RecruitmentStatus)
            .HasForeignKey(k => k.RecruitmentStatusId)
            .HasConstraintName($"{nameof(RecruitmentStatus)}_{nameof(Recruitment)}_FK")
            .OnDelete(DeleteBehavior.Restrict);

        var data = SharedRecruitmentStatus.All.Select(i => new RecruitmentStatus
        {
            RecruitmentStatusId = i.Id,
            Name = i.Name,
        });
        builder.HasData(data);
    }
}