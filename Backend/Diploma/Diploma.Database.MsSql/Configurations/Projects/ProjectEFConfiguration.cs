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


        builder
           .HasOne(k => k.Root)
           .WithMany(k => k.History)
           .HasForeignKey(k => k.RootId)
           .HasConstraintName($"{nameof(Project)}_{nameof(Project)}_ROOT_FK")
           .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.Next)
            .WithOne(k => k.Previous)
            .HasForeignKey<Project>(k => k.NextId)
            .HasConstraintName($"{nameof(Project)}_{nameof(Project)}_NEXT_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}