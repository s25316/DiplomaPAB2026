using Diploma.Application.Interfaces.Repositories;
using Diploma.Application.Interfaces.Smtp;
using Diploma.Application.Persons.Interfaces;
using Diploma.Domain.ValueObjects;

namespace Diploma.Application.Services;

public record EmailServiceInput
{
    public required PersonOperationId PersonOperationId { get; init; }
    public required Email Email { get; init; }
    public required string Subject { get; init; }
    public required string Body { get; init; }
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.Now;
}

public interface IEmailService
{
    Task CreateAndSendAsync(
        EmailServiceInput input,
        CancellationToken cancellationToken = default);
}

public class EmailService(
    ISmtpService smtpService,
    IEmailRespository emailRespository
    ) : IEmailService
{
    public async Task CreateAndSendAsync(
        EmailServiceInput input,
        CancellationToken cancellationToken = default)
    {
        await smtpService.SendAsync(new SmtpServiceInput
        {
            Email = input.Email,
            Subject = input.Subject,
            Body = input.Body,
        }, cancellationToken);

        await emailRespository.CreateAsync(new EmailRespositoryInput
        {
            OperationId = input.PersonOperationId.Value,
            Email = input.Email,
            Subject = input.Subject,
            Body = input.Body,
            CreatedAt = input.CreatedAt,
            DeliveredAt = DateTimeOffset.Now,
        }, cancellationToken);
    }
}