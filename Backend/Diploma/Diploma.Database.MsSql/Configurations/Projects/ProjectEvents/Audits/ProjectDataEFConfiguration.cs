using Diploma.Database.Models.Projects.ProjectEvents;
using Diploma.Database.Models.Projects.ProjectEvents.Audits;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.Database.MsSql.Configurations.Projects.ProjectEvents.Audits;

public class ProjectDataEFConfiguration : IEntityTypeConfiguration<ProjectData>
{
    public void Configure(EntityTypeBuilder<ProjectData> builder)
    {
        builder.ToTable(nameof(ProjectData));
        builder
            .HasKey(k => k.ProjectDataId)
            .HasName($"{nameof(ProjectData)}_PK");

        builder
            .Property(p => p.ProjectDataId)
            .HasDefaultValueSql(DefaultValue.GUID);


        builder
            .HasOne(k => k.ProjectEvent)
            .WithOne(k => k.ProjectData)
            .HasForeignKey<ProjectData>(k => k.ProjectEventId)
            .HasConstraintName($"{nameof(ProjectEvent)}_{nameof(ProjectData)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}