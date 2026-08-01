using Diploma.Database.Models.Projects.ProjectEvents.Audits.ProjectVisibilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedProjectVisibility = Diploma.Shared.ProjectVisibilities.ProjectVisibility;

namespace Diploma.Database.MsSql.Configurations.Projects.ProjectEvents.Audits.ProjectVisibilities;

public class ProjectVisibilityTypeEFConfiguration : IEntityTypeConfiguration<ProjectVisibilityType>
{
    public void Configure(EntityTypeBuilder<ProjectVisibilityType> builder)
    {
        builder.ToTable(nameof(ProjectVisibilityType));
        builder
            .HasKey(k => k.ProjectVisibilityTypeId)
            .HasName($"{nameof(ProjectVisibilityType)}_PK");

        builder
            .Property(p => p.ProjectVisibilityTypeId)
            .ValueGeneratedNever();


        builder
            .HasMany(k => k.ProjectVisibilities)
            .WithOne(k => k.ProjectVisibilityType)
            .HasForeignKey(k => k.ProjectVisibilityTypeId)
            .HasConstraintName($"{nameof(ProjectVisibility)}_{nameof(ProjectVisibilityType)}_FK")
            .OnDelete(DeleteBehavior.Restrict);


        var data = SharedProjectVisibility.All.Select(i => new ProjectVisibilityType
        {
            ProjectVisibilityTypeId = i.Id,
            Name = i.Name,
        });
        builder.HasData(data);
    }
}