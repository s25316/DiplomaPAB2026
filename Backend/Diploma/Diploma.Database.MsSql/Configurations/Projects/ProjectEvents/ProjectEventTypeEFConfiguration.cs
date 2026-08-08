using Diploma.Database.Models.Projects.ProjectEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedProjectEvent = Diploma.Shared.ProjectEvents.ProjectEvent;

namespace Diploma.Database.MsSql.Configurations.Projects.ProjectEvents;

public class ProjectEventTypeEFConfiguration : IEntityTypeConfiguration<ProjectEventType>
{
    public void Configure(EntityTypeBuilder<ProjectEventType> builder)
    {
        builder.ToTable(nameof(ProjectEventType));
        builder
            .HasKey(k => k.ProjectEventTypeId)
            .HasName($"{nameof(ProjectEventType)}_PK");

        builder
            .Property(p => p.ProjectEventTypeId)
            .ValueGeneratedNever();


        builder
            .HasMany(k => k.ProjectEvents)
            .WithOne(k => k.ProjectEventType)
            .HasForeignKey(k => k.ProjectEventTypeId)
            .HasConstraintName($"{nameof(ProjectEventType)}_{nameof(ProjectEvent)}_FK")
            .OnDelete(DeleteBehavior.Cascade);


        var data = SharedProjectEvent.All.Select(i => new ProjectEventType
        {
            ProjectEventTypeId = i.Id,
            Name = i.Name,
        });
        builder.HasData(data);
    }
}