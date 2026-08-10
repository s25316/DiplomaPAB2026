using Diploma.Database.Models.Projects;
using Diploma.Database.Models.Projects.ProjectEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.Database.MsSql.Configurations.Projects;

internal class ProjectDataEFConfiguration : IEntityTypeConfiguration<ProjectData>
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
            .HasMany(i => i.Projects)
            .WithOne(i => i.LastProjectData)
            .HasForeignKey(i => i.LastProjectDataId)
            .HasConstraintName($"{nameof(ProjectData)}_{nameof(Project)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(i => i.ProjectEvent)
            .WithOne(i => i.ProjectData)
            .HasForeignKey<ProjectData>(i => i.ProjectEventId)
            .HasConstraintName($"{nameof(ProjectData)}_{nameof(ProjectEvent)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
           .HasOne(k => k.Root)
           .WithMany(k => k.History)
           .HasForeignKey(k => k.RootId)
           .HasConstraintName($"{nameof(ProjectData)}_{nameof(ProjectData)}_ROOT_FK")
           .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.Next)
            .WithOne(k => k.Previous)
            .HasForeignKey<ProjectData>(k => k.NextId)
            .HasConstraintName($"{nameof(ProjectData)}_{nameof(ProjectData)}_NEXT_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}