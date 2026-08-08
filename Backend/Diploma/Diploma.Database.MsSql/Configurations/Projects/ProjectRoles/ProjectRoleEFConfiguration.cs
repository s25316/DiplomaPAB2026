using Diploma.Database.Models.Projects.ProjectEvents;
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
            .HasOne(k => k.ProjectEvent)
            .WithOne(k => k.ProjectRole)
            .HasForeignKey<ProjectRole>(k => k.ProjectEventId)
            .HasConstraintName($"{nameof(ProjectRole)}_{nameof(ProjectEvent)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
           .HasOne(k => k.Root)
           .WithMany(k => k.History)
           .HasForeignKey(k => k.RootId)
           .HasConstraintName($"{nameof(ProjectRole)}_{nameof(ProjectRole)}_ROOT_FK")
           .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.Next)
            .WithOne(k => k.Previous)
            .HasForeignKey<ProjectRole>(k => k.NextId)
            .HasConstraintName($"{nameof(ProjectRole)}_{nameof(ProjectRole)}_NEXT_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}