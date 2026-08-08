using Diploma.Database.Models.Projects.ProjectRoles;
using Diploma.Database.Models.Projects.Recruitments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.Database.MsSql.Configurations.Projects.Recruitments;

public class RecruitmentProjectRoleEFConfiguration : IEntityTypeConfiguration<RecruitmentProjectRole>
{
    public void Configure(EntityTypeBuilder<RecruitmentProjectRole> builder)
    {
        builder.ToTable(nameof(RecruitmentProjectRole));
        builder
            .HasKey(k => k.RecruitmentProjectRoleId)
            .HasName($"{nameof(RecruitmentProjectRole)}_PK");


        builder
            .HasOne(k => k.Recruitment)
            .WithMany(k => k.RecruitmentProjectRoles)
            .HasForeignKey(k => k.RecruitmentId)
            .HasConstraintName($"{nameof(RecruitmentProjectRole)}_{nameof(Recruitment)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.ProjectRole)
            .WithMany(k => k.RecruitmentProjectRoles)
            .HasForeignKey(k => k.ProjectRoleId)
            .HasConstraintName($"{nameof(RecruitmentProjectRole)}_{nameof(ProjectRole)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}