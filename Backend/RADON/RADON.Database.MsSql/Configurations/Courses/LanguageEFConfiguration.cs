using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RADON.Database.MsSql;

namespace RADON.Database.Models.Courses;

public class LanguageEFConfiguration : IEntityTypeConfiguration<Language>
{
    public string LanguageCode { get; set; } = null!;
    public string Name { get; set; } = null!;


    public virtual ICollection<CourseInstanceEFConfiguration> CourseInstancesWithMainLanguage { get; set; } = [];
    public virtual ICollection<CourseInstanceEFConfiguration> CourseInstancesWithPhilologicalLanguages { get; set; } = [];

    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.ToTable(nameof(Language));
        builder
            .HasKey(k => k.LanguageCode)
            .HasName($"{nameof(Language)}_PK");

        builder
            .Property(p => p.LanguageCode)
            .HasMaxLength(DefaultValue.LENGTH_100);
        builder
            .Property(p => p.Name)
            .HasMaxLength(DefaultValue.LENGTH_100);


        builder
            .HasMany(k => k.CourseInstances)
            .WithOne(k => k.Language)
            .HasForeignKey(k => k.LanguageCode)
            .HasConstraintName($"{nameof(CourseInstance)}_{nameof(Language)}_FK")
            .OnDelete(DeleteBehavior.Restrict);

        var tableName = $"{nameof(CourseInstance)}{nameof(Language)}";
        builder
            .HasMany(k => k.CourseInstancesPhilological)
            .WithMany(k => k.PhilologicalLanguages)
            .UsingEntity<Dictionary<string, object>>(
                tableName,
                l => l
                .HasOne<CourseInstance>()
                .WithMany()
                .HasForeignKey($"{nameof(CourseInstance.CourseInstanceUuid)}")
                .HasConstraintName($"{tableName}_{nameof(CourseInstance)}_FK")
                .OnDelete(DeleteBehavior.Cascade),
                r => r
                .HasOne<Language>()
                .WithMany()
                .HasForeignKey($"{nameof(Language.LanguageCode)}")
                .HasConstraintName($"{tableName}_{nameof(Language)}_FK")
                .OnDelete(DeleteBehavior.Cascade));
    }
}