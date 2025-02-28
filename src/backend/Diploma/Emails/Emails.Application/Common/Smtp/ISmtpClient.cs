using MimeKit;

namespace Emails.Application.Common.Smtp;

/// <summary>
/// Использование SMTP клиента
/// </summary>
public interface ISmtpClient
{
    /// <summary>
    /// Отправить сообщение
    /// </summary>
    public Task SendAsync(MimeMessage message, int mailId);
}