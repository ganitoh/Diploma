using Common.Domain;
using Notifications.Domain.Enums;

namespace Notifications.Domain.Models;

/// <summary>
/// Уведомление
/// </summary>
public class Notification : Entity<int>
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Тип уведомления
    /// </summary>
    public NotificationType Type { get; set; }
    
    /// <summary>
    /// Указывает, требуется ли отправить уведомление на электронную почту.
    /// </summary>
    public bool IsEmailNotificationRequired { get; set; }
    
    /// <summary>
    /// Заголовок уведомления.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Текст уведомления
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// Адрес электронной почты пользователя.
    /// </summary>
    public string? Email { get; set; }
    
    /// <summary>
    /// Дата создания.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Дата отправки.
    /// </summary>
    public DateTime? SentDate { get; set; }

    /// <summary>
    /// Дата прочтения.
    /// </summary>
    public DateTime? ReadDate { get; set; }
}