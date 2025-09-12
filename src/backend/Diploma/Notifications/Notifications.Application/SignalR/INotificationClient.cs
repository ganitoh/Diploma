using Notifications.ApplicationContract.Dtos;

namespace Notifications.Application.SignalR;

public interface INotificationClient
{
    public Task ReceiveNotifications(NotificationDto notification);
}