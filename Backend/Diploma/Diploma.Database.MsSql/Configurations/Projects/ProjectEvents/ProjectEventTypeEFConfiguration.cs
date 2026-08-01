using Diploma.Database.Models.Projects.ProjectEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
            .HasConstraintName($"{nameof(ProjectEvent)}_{nameof(ProjectEventType)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
