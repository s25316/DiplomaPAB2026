using Diploma.Database.Models.Educations;
using Diploma.Database.Models.Projects.ProjectEvents;
using Diploma.Database.Models.Projects.ProjectRoles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.Database.MsSql.Configurations.Projects.ProjectRoles;

public class ProjectRoleEducationInstitutionEFConfiguration : IEntityTypeConfiguration<ProjectRoleEducationInstitution>
{
    public void Configure(EntityTypeBuilder<ProjectRoleEducationInstitution> builder)
    {
        builder.ToTable(nameof(ProjectRoleEducationInstitution));
        builder
            .HasKey(k => k.ProjectRoleEducationInstitutionId)
            .HasName($"{nameof(ProjectRoleEducationInstitution)}_PK");

        builder
            .Property(p => p.ProjectRoleEducationInstitutionId)
            .HasDefaultValueSql(DefaultValue.GUID);


        builder
            .HasOne(k => k.ProjectRole)
            .WithMany(k => k.ProjectRoleEducationInstitutions)
            .HasForeignKey(k => k.ProjectRoleId)
            .HasConstraintName($"{nameof(ProjectRoleEducationInstitution)}_{nameof(ProjectRole)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.EducationInstitution)
            .WithMany(k => k.ProjectRoleEducationInstitutions)
            .HasForeignKey(k => k.EducationInstitutionId)
            .HasConstraintName($"{nameof(ProjectRoleEducationInstitution)}_{nameof(EducationInstitution)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.AddProjectEvent)
            .WithOne(k => k.AddProjectRoleEducationInstitution)
            .HasForeignKey<ProjectRoleEducationInstitution>(k => k.AddProjectEventId)
            .HasConstraintName($"{nameof(ProjectRoleEducationInstitution)}_{nameof(ProjectEvent)}_ADD_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.RemoveProjectEvent)
            .WithOne(k => k.RemoveProjectRoleEducationInstitution)
            .HasForeignKey<ProjectRoleEducationInstitution>(k => k.RemoveProjectEventId)
            .HasConstraintName($"{nameof(ProjectRoleEducationInstitution)}_{nameof(ProjectEvent)}_REMOVE_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}