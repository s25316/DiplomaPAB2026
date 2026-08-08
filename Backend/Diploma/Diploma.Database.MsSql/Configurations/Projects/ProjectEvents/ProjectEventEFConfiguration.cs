using Diploma.Database.Models.Persons;
using Diploma.Database.Models.Projects;
using Diploma.Database.Models.Projects.ProjectEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.Database.MsSql.Configurations.Projects.ProjectEvents;

public class ProjectEventEFConfiguration : IEntityTypeConfiguration<ProjectEvent>
{
    public void Configure(EntityTypeBuilder<ProjectEvent> builder)
    {
        builder.ToTable(nameof(ProjectEvent));
        builder
            .HasKey(k => k.ProjectEventId)
            .HasName($"{nameof(ProjectEvent)}_PK");

        builder
            .Property(p => p.ProjectEventId)
            .HasDefaultValueSql(DefaultValue.GUID);


        builder
            .HasOne(k => k.Person)
            .WithMany(k => k.ProjectEvents)
            .HasForeignKey(k => k.PersonId)
            .HasConstraintName($"{nameof(ProjectEvent)}_{nameof(Person)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.Project)
            .WithMany(k => k.ProjectEvents)
            .HasForeignKey(k => k.ProjectId)
            .HasConstraintName($"{nameof(ProjectEvent)}_{nameof(Project)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}