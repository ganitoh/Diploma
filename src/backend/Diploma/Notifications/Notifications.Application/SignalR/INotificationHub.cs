using Notifications.ApplicationContract.Dtos;

namespace Notifications.Application.SignalR;

/// <summary>
/// Hub для работы с уведомлениями
/// </summary>
public interface INotificationHub
{
    /// <summary>
    /// Отправить уведомление
    /// </summary>
    Task SendNotification(NotificationDto request, CancellationToken cancellationToken = default);
}