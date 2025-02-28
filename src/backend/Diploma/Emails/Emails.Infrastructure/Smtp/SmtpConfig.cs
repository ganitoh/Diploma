using System.Globalization;

namespace Emails.Infrastructure.Smtp;

/// <summary>
/// Настройка SMTP
/// </summary>
public class SmtpConfig
{
    /// <summary>
    /// Название почтового ящика
    /// </summary>
    public string MailBoxName { get; set; }
    
    /// <summary>
    /// Адрес сервера
    /// </summary>
    public string SmtpServer { get; set; }

    /// <summary>
    /// Порт сервера
    /// </summary>
    public int SmtpPort { get; set; }

    /// <summary>
    /// Логин сервера
    /// </summary>
    public string SmtpUsername { get; set; }

    /// <summary>
    /// Пароль сервера
    /// </summary>
    public string SmtpPassword { get; set; }
    
    /// <summary>
    /// Включить SSL
    /// </summary>
    public bool EnableSSl { get; set; }
}