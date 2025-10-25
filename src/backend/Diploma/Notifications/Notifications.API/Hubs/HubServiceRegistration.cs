using Notifications.Application.SignalR;

namespace Notifications.API.Hubs;

public static class HubServiceRegistration
{
    public static IServiceCollection AddHubs(this IServiceCollection services)
    {
        services.AddScoped<INotificationHub, NotificationHub>();
        services.AddScoped<IHubFactory, HubFactory>();
        return services;
    }
    
    public static IEndpointRouteBuilder UseHubs(this IEndpointRouteBuilder router)
    {
        router.MapHub<NotificationHub>("/api/v1/hubs/notification");
        return router;
    }
}