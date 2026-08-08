using Diploma.Database.Models.Persons;
using Diploma.Database.Models.Projects.ProjectEvents;
using Diploma.Database.Models.Projects.ProjectManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.Database.MsSql.Configurations.Projects.ProjectManagers;

public class ProjectManagerEFConfiguration : IEntityTypeConfiguration<ProjectManager>
{
    public void Configure(EntityTypeBuilder<ProjectManager> builder)
    {
        builder.ToTable(nameof(ProjectManager));
        builder
            .HasKey(k => k.ProjectManagerId)
            .HasName($"{nameof(ProjectManager)}_PK");

        builder
            .Property(p => p.ProjectManagerId)
            .HasDefaultValueSql(DefaultValue.GUID);


        builder
            .HasOne(k => k.Person)
            .WithMany(k => k.ProjectManagers)
            .HasForeignKey(k => k.PersonId)
            .HasConstraintName($"{nameof(ProjectManager)}_{nameof(Person)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.GrantEvent)
            .WithOne(k => k.GrantProjectManager)
            .HasForeignKey<ProjectManager>(k => k.GrantEventId)
            .HasConstraintName($"{nameof(ProjectManager)}_{nameof(ProjectEvent)}_GRANT_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.RevokeEvent)
            .WithOne(k => k.RevokeProjectManager)
            .HasForeignKey<ProjectManager>(k => k.RevokeEventId)
            .HasConstraintName($"{nameof(ProjectManager)}_{nameof(ProjectEvent)}_REVOKE_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}