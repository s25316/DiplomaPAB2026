using Diploma.Database.Models.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.Database.MsSql.Configurations.Projects;

public class ProjectEFConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable(nameof(Project));
        builder
            .HasKey(k => k.ProjectId)
            .HasName($"{nameof(Project)}_PK");

        builder
            .Property(p => p.ProjectId)
            .HasDefaultValueSql(DefaultValue.GUID);
    }
}