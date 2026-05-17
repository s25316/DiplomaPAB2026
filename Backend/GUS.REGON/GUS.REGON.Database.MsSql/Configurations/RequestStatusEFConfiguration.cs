using GUS.REGON.Database.Enums;
using GUS.REGON.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GUS.REGON.Database.MsSql.Configurations;

public class RequestStatusEFConfiguration : IEntityTypeConfiguration<RequestStatus>
{
    public void Configure(EntityTypeBuilder<RequestStatus> builder)
    {
        builder.ToTable(nameof(RequestStatus));
        builder
            .HasKey(k => k.RequestStatusCode)
            .HasName($"{nameof(RequestStatus)}_PK");

        builder
            .Property(k => k.RequestStatusCode)
            .ValueGeneratedNever();

        builder
            .HasMany(k => k.Requests)
            .WithOne(k => k.RequestStatus)
            .HasForeignKey(k => k.RequestStatusCode)
            .HasConstraintName($"{nameof(Request)}_{nameof(RequestStatus)}_FK")
            .OnDelete(DeleteBehavior.Restrict);

        var data = new List<RequestStatus>
        {
            new() { RequestStatusCode = (int)RequestStatusCode.Istneje, Name = "Istneje" },
            new() { RequestStatusCode = (int)RequestStatusCode.NieIstneje, Name = "Nie Istneje" },
            new() { RequestStatusCode = (int)RequestStatusCode.BrakUprawnien, Name = "Brak Uprawnien" },
        };
        builder.HasData(data);
    }
}