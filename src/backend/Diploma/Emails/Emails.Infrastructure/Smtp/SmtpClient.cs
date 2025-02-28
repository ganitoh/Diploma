using Emails.Application.Common.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Emails.Infrastructure.Smtp;

public class SmtpClient : ISmtpClient
{
    private readonly ILogger<SmtpClient> _logger;
    private readonly SmtpConfig _smtpConfig;

    public SmtpClient(ILogger<SmtpClient> logger, IOptions<SmtpConfig> smtpOptions)
    {
        _logger = logger;
        _smtpConfig = smtpOptions.Value;
    }

    public async Task SendAsync(MimeMessage message, int mailId)
    {
        try
        {
            using var client = new MailKit.Net.Smtp.SmtpClient();

            var socketOptions = SecureSocketOptions.None;

            if (_smtpConfig.EnableSSl)
            {
                client.RequireTLS = true;
                socketOptions = SecureSocketOptions.StartTls;
            }

            await client.ConnectAsync(_smtpConfig.SmtpServer, _smtpConfig.SmtpPort, socketOptions);
            
            if (CanAuthenticate())
            {
                _logger.LogInformation("Supported mechanisms {0}", string.Join(',', client.AuthenticationMechanisms));
                
                if (client.AuthenticationMechanisms.Contains("NTLM"))
                {
                    _logger.LogInformation("Authenticate using NTLM");
                    var ntlm = new SaslMechanismNtlm(_smtpConfig.SmtpUsername, _smtpConfig.SmtpPassword);
                    await client.AuthenticateAsync(ntlm);
                }
                else
                {
                    await client.AuthenticateAsync(_smtpConfig.SmtpUsername, _smtpConfig.SmtpPassword);
                }
            }

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
        catch (NotSupportedException e)
        {
            _logger.LogError(e,
                "Smtp server EPC can not authentication. It is normal. The message with id {mailId} will send next time.",
                mailId);

            throw new AuthenticationException(
                $"Smtp server EPC can not authentication. It is normal. The message with id {mailId} will send next time.",
                e);
        }
        catch (TimeoutException e)
        {
            _logger.LogError(e,
                "Operation timed out after 120000 milliseconds. It is normal. The message with id {mailId} will send next time.",
                mailId);

            throw;
        }
        catch (Exception e)
        {
            if (e.Message.Contains("authentication", StringComparison.InvariantCultureIgnoreCase))
            {
                _logger.LogError(e,
                    "Smtp server EPC can not authentication. It is normal. The message with id {mailId} will send next time.",
                    mailId);

                throw new AuthenticationException(
                    $"Smtp server EPC can not authentication. It is normal. The message with id {mailId} will send next time.",
                    e);
            }

            _logger.LogError(e, "SmtpClient has error with email id: {mailId}, to: {mailTo}", mailId, message.To);

            throw;
        }
    }
    
    private bool CanAuthenticate() =>
        !string.IsNullOrWhiteSpace(_smtpConfig.SmtpUsername) &&
        !string.IsNullOrWhiteSpace(_smtpConfig.SmtpPassword);
}