using Diploma.Database.Models.Projects.ProjectEvents.Audits.ProjectManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedProjectManager = Diploma.Shared.ProjectManagers.ProjectManager;

namespace Diploma.Database.MsSql.Configurations.Projects.ProjectEvents.Audits.ProjectManagers;

public class ProjectManagerTypeEFConfiguration : IEntityTypeConfiguration<ProjectManagerType>
{
    public void Configure(EntityTypeBuilder<ProjectManagerType> builder)
    {
        builder.ToTable(nameof(ProjectManagerType));
        builder
            .HasKey(k => k.ProjectManagerTypeId)
            .HasName($"{nameof(ProjectManagerType)}_PK");

        builder
            .Property(p => p.ProjectManagerTypeId)
            .ValueGeneratedNever();


        builder
            .HasMany(k => k.ProjectManagers)
            .WithOne(k => k.ProjectManagerType)
            .HasForeignKey(k => k.ProjectManagerTypeId)
            .HasConstraintName($"{nameof(ProjectManager)}_{nameof(ProjectManagerType)}_FK")
            .OnDelete(DeleteBehavior.Restrict);


        var data = SharedProjectManager.All.Select(i => new ProjectManagerType
        {
            ProjectManagerTypeId = i.Id,
            Name = i.Name,
        });
        builder.HasData(data);
    }
}