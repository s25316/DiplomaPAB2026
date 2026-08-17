using Diploma.Database.Models.Persons;
using Diploma.Database.Models.Projects;
using Diploma.Database.Models.Projects.Recruitments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.Database.MsSql.Configurations.Projects.Recruitments;

public class RecruitmentEFConfiguration : IEntityTypeConfiguration<Recruitment>
{
    public void Configure(EntityTypeBuilder<Recruitment> builder)
    {
        builder.ToTable(nameof(Recruitment));
        builder
            .HasKey(k => k.RecruitmentId)
            .HasName($"{nameof(Recruitment)}_PK");


        builder
           .HasOne(k => k.Person)
           .WithMany(k => k.Recruitments)
           .HasForeignKey(k => k.PersonId)
           .HasConstraintName($"{nameof(Recruitment)}_{nameof(Person)}_FK")
           .OnDelete(DeleteBehavior.Restrict);
        builder
           .HasOne(k => k.Project)
           .WithMany(k => k.Recruitments)
           .HasForeignKey(k => k.ProjectId)
           .HasConstraintName($"{nameof(Recruitment)}_{nameof(Project)}_FK")
           .OnDelete(DeleteBehavior.Restrict);
    }
}