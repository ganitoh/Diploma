using Emails.Domain.Models;

namespace Emails.Application.Common.Smtp;

public interface IMailService
{
    Task SendEmailAsync(Mail mail, CancellationToken cancellationToken);
}