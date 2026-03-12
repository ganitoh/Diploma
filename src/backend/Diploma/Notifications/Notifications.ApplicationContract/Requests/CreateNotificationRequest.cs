using Notifications.Domain.Enums;

namespace Notifications.ApplicationContract.Requests;

/// <summary>
/// Данные для создания уведомления
/// </summary>
public class CreateNotificationRequest
{
    /// <summary>
    /// Идентификатор пользовтелей кому адресовано сообщение
    /// </summary>
    public Guid[] UsersIds { get; set; }

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
    /// Отправить сразу
    /// </summary>
    public bool IsSendImmediately { get; set; } = true;

    public CreateNotificationRequest() { }
    public CreateNotificationRequest(Guid[] usersIds, NotificationType type, bool isEmailNotificationRequired, string? title, string? text,  bool isSendImmediately)
    {
        UsersIds = usersIds;
        Type = type;
        IsEmailNotificationRequired = isEmailNotificationRequired;
        Title = title;
        Text = text;
        IsSendImmediately = isSendImmediately;
    }
}