using Diploma.Database.Models.Persons;
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
           .HasOne(k => k.Root)
           .WithMany(k => k.History)
           .HasForeignKey(k => k.RootId)
           .HasConstraintName($"{nameof(Recruitment)}_{nameof(Recruitment)}_ROOT_FK")
           .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(k => k.Next)
            .WithOne(k => k.Previous)
            .HasForeignKey<Recruitment>(k => k.NextId)
            .HasConstraintName($"{nameof(Recruitment)}_{nameof(Recruitment)}_NEXT_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}