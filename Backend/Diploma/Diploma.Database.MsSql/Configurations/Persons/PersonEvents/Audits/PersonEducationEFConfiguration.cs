using Diploma.Database.Models.Educations;
using Diploma.Database.Models.Persons.PersonEvents;
using Diploma.Database.Models.Persons.PersonEvents.Audits;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diploma.Database.MsSql.Configurations.Persons.PersonEvents.Audits;

public class PersonEducationEFConfiguration : IEntityTypeConfiguration<PersonEducation>
{
    public void Configure(EntityTypeBuilder<PersonEducation> builder)
    {
        builder.ToTable(nameof(PersonEducation));
        builder
            .HasKey(k => k.PersonEducationId)
            .HasName($"{nameof(PersonEducation)}_PK");

        builder
            .Property(p => p.PersonEducationId)
            .HasDefaultValueSql(DefaultValue.GUID);


        builder
            .HasOne(k => k.PersonEvent)
            .WithOne(k => k.PersonEducation)
            .HasForeignKey<PersonEducation>(k => k.PersonEventId)
            .HasConstraintName($"{nameof(PersonEvent)}_{nameof(PersonEducation)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.Root)
            .WithMany(k => k.History)
            .HasForeignKey(k => k.RootId)
            .HasConstraintName($"{nameof(PersonEducation)}_{nameof(PersonEducation)}_ROOT_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.Next)
            .WithOne(k => k.Previous)
            .HasForeignKey<PersonEducation>(k => k.NextId)
            .HasConstraintName($"{nameof(PersonEducation)}_{nameof(PersonEducation)}_NEXT_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.EducationCourse)
            .WithMany(k => k.PersonEducations)
            .HasForeignKey(k => k.EducationCourseId)
            .HasConstraintName($"{nameof(PersonEducation)}_{nameof(EducationCourse)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.EducationCourseInstance)
            .WithMany(k => k.PersonEducations)
            .HasForeignKey(k => k.EducationCourseInstanceId)
            .HasConstraintName($"{nameof(PersonEducation)}_{nameof(EducationCourseInstance)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}