using Common.Domain;

namespace Emails.Domain.Models;

/// <summary>
/// Почтовое сообщение
/// </summary>
public class Mail : Entity<int>
{
    /// <summary>
    /// Тема
    /// </summary>
    public string Subject { get; set; }
    
    /// <summary>
    /// Тело сообщения
    /// </summary>
    public string Body { get; set; }

    /// <summary>
    /// Отправитель
    /// </summary>
    public string From { get; set; }

    /// <summary>
    /// Получатель
    /// </summary>
    public string To { get; set; }

    /// <summary>
    /// Флаг отправки
    /// </summary>
    public bool IsSent { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreateDate { get; set; }
}