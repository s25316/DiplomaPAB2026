using Diploma.Application.Interfaces.Repositories;
using Diploma.Database;
using Diploma.Database.Models.Shared;

namespace Diploma.Infrastructure.Services.Repositories;

public class EmailRespository(
    DiplomaDbContext context
    ) : IEmailRespository
{
    public async Task CreateAsync(
        EmailRespositoryInput input,
        CancellationToken cancellationToken = default)
    {
        var databaseEmial = new EmailMessage
        {
            PersonOperationId = input.OperationId,
            Email = input.Email.Value,
            Subject = input.Subject,
            Body = input.Body,
            CreatedAt = input.CreatedAt,
            DeliveredAt = input.DeliveredAt,
        };

        await context.EmailMessages.AddAsync(databaseEmial, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}