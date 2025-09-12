using Notifications.Application.SignalR;

namespace Notifications.API.Hubs;

/// <summary>
/// Фабрика для получения hub-ов
/// </summary>
public class HubFactory(IServiceProvider serviceProvider) : IHubFactory
{
    /// <inheritdoc />
    public INotificationHub GetNotificationHub() 
        => serviceProvider.GetRequiredService<INotificationHub>();
}