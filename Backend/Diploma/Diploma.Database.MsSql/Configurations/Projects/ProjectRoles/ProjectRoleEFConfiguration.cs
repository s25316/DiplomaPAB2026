using Diploma.Database.Models.Projects;
using Diploma.Database.Models.Projects.ProjectRoles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.Database.MsSql.Configurations.Projects.ProjectRoles;

public class ProjectRoleEFConfiguration : IEntityTypeConfiguration<ProjectRole>
{
    public void Configure(EntityTypeBuilder<ProjectRole> builder)
    {
        builder.ToTable(nameof(ProjectRole));
        builder
            .HasKey(k => k.ProjectRoleId)
            .HasName($"{nameof(ProjectRole)}_PK");

        builder
            .Property(k => k.ProjectRoleId)
            .HasDefaultValueSql(DefaultValue.GUID);


        builder
            .HasOne(k => k.Project)
            .WithMany(k => k.ProjectRoles)
            .HasForeignKey(k => k.ProjectId)
            .HasConstraintName($"{nameof(ProjectRole)}_{nameof(Project)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}