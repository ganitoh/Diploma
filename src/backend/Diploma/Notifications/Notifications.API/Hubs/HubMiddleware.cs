namespace Notifications.API.Hubs;

public static class HubMiddleware
{
    public static IEndpointRouteBuilder UseHubs(this IEndpointRouteBuilder router)
    {
        router.MapHub<NotificationHub>("/api/v1/hubs/notification");
        return router;
    }
}