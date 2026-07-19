using Diploma.Application.Interfaces.Smtp;
using Diploma.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Diploma.Infrastructure.Services.Smtp;

public class SmtpService(IOptions<EmailConfiguration> options) : ISmtpService
{
    public async Task SendAsync(SmtpServiceInput input, CancellationToken cancellationToken = default)
    {
        var configuration = options.Value;

        using var smtp = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(configuration.Email, configuration.Key)
        };

        using var mail = new MailMessage(configuration.Email, input.Email.Value)
        {
            Subject = input.Subject,
            Body = input.Body,
            IsBodyHtml = true,
        };

        await smtp.SendMailAsync(mail, cancellationToken);
    }
}