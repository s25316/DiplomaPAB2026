using Diploma.Database.Models.Projects.ProjectEvents;
using Diploma.Database.Models.Projects.ProjectRoles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.Database.MsSql.Configurations.Projects.ProjectRoles;

public class ProjectRoleDataEFConfiguration : IEntityTypeConfiguration<ProjectRoleData>
{
    public void Configure(EntityTypeBuilder<ProjectRoleData> builder)
    {
        builder.ToTable(nameof(ProjectRoleData));
        builder
            .HasKey(k => k.ProjectRoleDataId)
            .HasName($"{nameof(ProjectRoleData)}_PK");

        builder
            .Property(k => k.ProjectRoleDataId)
            .HasDefaultValueSql(DefaultValue.GUID);


        builder
            .HasMany(k => k.ProjectRoles)
            .WithOne(k => k.LastProjectRoleData)
            .HasForeignKey(k => k.LastProjectRoleDataId)
            .HasConstraintName($"{nameof(ProjectRole)}_{nameof(ProjectRoleData)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.ProjectRole)
            .WithMany(k => k.ProjectRoleDatas)
            .HasForeignKey(k => k.ProjectRoleId)
            .HasConstraintName($"{nameof(ProjectRoleData)}_{nameof(ProjectRole)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.ProjectEvent)
            .WithOne(k => k.ProjectRoleData)
            .HasForeignKey<ProjectRoleData>(k => k.ProjectEventId)
            .HasConstraintName($"{nameof(ProjectRoleData)}_{nameof(ProjectEvent)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
           .HasOne(k => k.Root)
           .WithMany(k => k.History)
           .HasForeignKey(k => k.RootId)
           .HasConstraintName($"{nameof(ProjectRoleData)}_{nameof(ProjectRoleData)}_ROOT_FK")
           .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.Next)
            .WithOne(k => k.Previous)
            .HasForeignKey<ProjectRoleData>(k => k.NextId)
            .HasConstraintName($"{nameof(ProjectRoleData)}_{nameof(ProjectRoleData)}_NEXT_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}