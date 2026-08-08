using Diploma.Database.Models.Educations;
using Diploma.Database.Models.Projects.ProjectEvents;
using Diploma.Database.Models.Projects.ProjectRoles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.Database.MsSql.Configurations.Projects.ProjectRoles;

public class ProjectRoleEducationCourseDisciplineEFConfiguration : IEntityTypeConfiguration<ProjectRoleEducationCourseDiscipline>
{
    public void Configure(EntityTypeBuilder<ProjectRoleEducationCourseDiscipline> builder)
    {
        builder.ToTable(nameof(ProjectRoleEducationCourseDiscipline));
        builder
            .HasKey(k => k.ProjectRoleEducationCourseDisciplineId)
            .HasName($"{nameof(ProjectRoleEducationCourseDiscipline)}_PK");

        builder
            .Property(p => p.ProjectRoleEducationCourseDisciplineId)
            .HasDefaultValueSql(DefaultValue.GUID);


        builder
            .HasOne(k => k.ProjectRole)
            .WithMany(k => k.ProjectRoleEducationCourseDisciplines)
            .HasForeignKey(k => k.ProjectRoleId)
            .HasConstraintName($"{nameof(ProjectRoleEducationCourseDiscipline)}_{nameof(ProjectRole)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.EducationCourseDiscipline)
            .WithMany(k => k.ProjectRoleEducationCourseDisciplines)
            .HasForeignKey(k => k.EducationCourseDisciplineId)
            .HasConstraintName($"{nameof(ProjectRoleEducationCourseDiscipline)}_{nameof(EducationCourseDiscipline)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.AddProjectEvent)
            .WithOne(k => k.AddProjectRoleEducationCourseDiscipline)
            .HasForeignKey<ProjectRoleEducationCourseDiscipline>(k => k.AddProjectEventId)
            .HasConstraintName($"{nameof(ProjectRoleEducationCourseDiscipline)}_{nameof(ProjectEvent)}_ADD_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.RemoveProjectEvent)
            .WithOne(k => k.RemoveProjectRoleEducationCourseDiscipline)
            .HasForeignKey<ProjectRoleEducationCourseDiscipline>(k => k.RemoveProjectEventId)
            .HasConstraintName($"{nameof(ProjectRoleEducationCourseDiscipline)}_{nameof(ProjectEvent)}_REMOVE_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}