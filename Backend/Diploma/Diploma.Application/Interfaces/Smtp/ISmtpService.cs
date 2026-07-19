using Diploma.Domain.ValueObjects;

namespace Diploma.Application.Interfaces.Smtp;

public sealed record SmtpServiceInput
{
    public required Email Email { get; init; }
    public required string Subject { get; init; }
    public required string Body { get; init; }
}

public interface ISmtpService
{
    Task SendAsync(
        SmtpServiceInput input,
        CancellationToken cancellationToken = default);
}