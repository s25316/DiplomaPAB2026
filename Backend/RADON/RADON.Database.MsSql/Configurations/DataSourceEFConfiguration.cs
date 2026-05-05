using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RADON.Database.Models;
using RADON.Database.Models.Courses;
using RADON.Database.Models.Institutions;

namespace RADON.Database.MsSql.Configurations;

internal class DataSourceEFConfiguration : IEntityTypeConfiguration<DataSource>
{
    public void Configure(EntityTypeBuilder<DataSource> builder)
    {
        builder.ToTable(nameof(DataSource));
        builder
            .HasKey(k => k.DataSourceId)
            .HasName($"{nameof(DataSource)}_PK");

        builder
            .Property(p => p.DataSourceId)
            .HasDefaultValueSql(DefaultValue.GUID);
        builder
            .Property(p => p.Name)
            .HasMaxLength(DefaultValue.LENGTH_100);


        builder
            .HasMany(k => k.Institutions)
            .WithOne(k => k.DataSource)
            .HasForeignKey(k => k.DataSourceId)
            .HasConstraintName($"{nameof(DataSource)}_{nameof(Institution)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasMany(k => k.Courses)
            .WithOne(k => k.DataSource)
            .HasForeignKey(k => k.DataSourceId)
            .HasConstraintName($"{nameof(DataSource)}_{nameof(Course)}_FK")
            .OnDelete(DeleteBehavior.Restrict);
    }
}