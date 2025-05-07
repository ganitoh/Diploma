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
    public string Host { get; set; }

    /// <summary>
    /// Порт сервера
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// Логин сервера
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Пароль сервера
    /// </summary>
    public string Password { get; set; }
    
    /// <summary>
    /// Включить SSL
    /// </summary>
    public bool EnableSSl { get; set; }
}