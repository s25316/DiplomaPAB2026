using Diploma.Database.Models.Projects.ProjectEvents;
using Diploma.Database.Models.Projects.ProjectEvents.Audits.ProjectVisibilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.Database.MsSql.Configurations.Projects.ProjectEvents.Audits.ProjectVisibilities;

public class ProjectVisibilityEFConfiguration : IEntityTypeConfiguration<ProjectVisibility>
{
    public void Configure(EntityTypeBuilder<ProjectVisibility> builder)
    {
        builder.ToTable(nameof(ProjectVisibility));
        builder
            .HasKey(k => k.ProjectVisibilityId)
            .HasName($"{nameof(ProjectVisibility)}_PK");

        builder
            .Property(p => p.ProjectVisibilityId)
            .HasDefaultValueSql(DefaultValue.GUID);


        builder
            .HasOne(k => k.ProjectEvent)
            .WithOne(k => k.ProjectVisibility)
            .HasForeignKey<ProjectVisibility>(k => k.ProjectEventId)
            .HasConstraintName($"{nameof(ProjectEvent)}_{nameof(ProjectVisibility)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}