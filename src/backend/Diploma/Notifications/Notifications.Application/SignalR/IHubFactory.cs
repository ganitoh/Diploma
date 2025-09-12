namespace Notifications.Application.SignalR;

/// <summary>
/// Абстракция фабрики для создания hub-ов.
/// </summary>
public interface IHubFactory
{
    /// <summary>
    /// Получить hub уведомлений
    /// </summary>
    /// <returns></returns>
    INotificationHub GetNotificationHub();
}