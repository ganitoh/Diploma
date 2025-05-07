using System.ComponentModel.DataAnnotations;
using Emails.Application.Common.Smtp;
using Emails.Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Emails.Infrastructure.Smtp;

public class MailService : IMailService
{
    private readonly ILogger<MailService> _logger;
    private readonly SmtpConfig _smtpConfig;

    public MailService(ILogger<MailService> logger, IOptions<SmtpConfig> smtpConfig)
    {
        _logger = logger;
        _smtpConfig = smtpConfig.Value;
    }

    public async Task SendEmailAsync(Mail mail, CancellationToken cancellationToken)
    {
        try
        {
            if (MailIsValid(mail))
            {
                using var emailData = new MimeMessage();
                
                emailData.From.Add(new MailboxAddress(_smtpConfig.MailBoxName, mail.From));

                var builder = new BodyBuilder();

                builder.HtmlBody = mail.Body;
                emailData.Subject = mail.Subject;

                emailData.Body = builder.ToMessageBody();

                using var smtp = new MailKit.Net.Smtp.SmtpClient();

                await smtp.ConnectAsync(_smtpConfig.Host, _smtpConfig.Port, _smtpConfig.EnableSSl, cancellationToken);
                await smtp.AuthenticateAsync(_smtpConfig.Username, _smtpConfig.Password, cancellationToken);
                await smtp.SendAsync(emailData, cancellationToken);
                await smtp.DisconnectAsync(true, cancellationToken);
            }
            else
                throw new ValidationException("Ошибка валидации");
        }
        catch (Exception e)
        {
            _logger.LogInformation(e, e.Message);
        }
    }

    private bool MailIsValid(Mail mail)
    {
        if (string.IsNullOrEmpty(mail.Body))
            return false;

        if (string.IsNullOrEmpty(mail.To))
            return false;
        
        if (string.IsNullOrEmpty(mail.From))
            return false;
        
        if (string.IsNullOrEmpty(mail.Subject))
            return false;

        return true;
    }
}