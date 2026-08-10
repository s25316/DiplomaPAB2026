using Diploma.Database.Models.Educations;
using Diploma.Database.Models.Projects.ProjectEvents;
using Diploma.Database.Models.Projects.ProjectRoles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.Database.MsSql.Configurations.Projects.ProjectRoles;

public class ProjectRoleEducationDisciplineEFConfiguration : IEntityTypeConfiguration<ProjectRoleEducationDiscipline>
{
    public void Configure(EntityTypeBuilder<ProjectRoleEducationDiscipline> builder)
    {
        builder.ToTable(nameof(ProjectRoleEducationDiscipline));
        builder
            .HasKey(k => k.ProjectRoleEducationDisciplineId)
            .HasName($"{nameof(ProjectRoleEducationDiscipline)}_PK");

        builder
            .Property(p => p.ProjectRoleEducationDisciplineId)
            .HasDefaultValueSql(DefaultValue.GUID);


        builder
            .HasOne(k => k.ProjectRole)
            .WithMany(k => k.ProjectRoleEducationCourseDisciplines)
            .HasForeignKey(k => k.ProjectRoleId)
            .HasConstraintName($"{nameof(ProjectRoleEducationDiscipline)}_{nameof(ProjectRole)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.EducationDiscipline)
            .WithMany(k => k.ProjectRoleEducationDisciplines)
            .HasForeignKey(k => k.EducationDisciplineCode)
            .HasConstraintName($"{nameof(ProjectRoleEducationDiscipline)}_{nameof(EducationCourseDiscipline)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.AddProjectEvent)
            .WithOne(k => k.AddProjectRoleEducationCourseDiscipline)
            .HasForeignKey<ProjectRoleEducationDiscipline>(k => k.AddProjectEventId)
            .HasConstraintName($"{nameof(ProjectRoleEducationDiscipline)}_{nameof(ProjectEvent)}_ADD_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.RemoveProjectEvent)
            .WithOne(k => k.RemoveProjectRoleEducationCourseDiscipline)
            .HasForeignKey<ProjectRoleEducationDiscipline>(k => k.RemoveProjectEventId)
            .HasConstraintName($"{nameof(ProjectRoleEducationDiscipline)}_{nameof(ProjectEvent)}_REMOVE_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}