namespace Chat.API.Hubs;

public static class HubMiddleware
{
    public static IEndpointRouteBuilder UseHubs(this IEndpointRouteBuilder router)
    {
        router.MapHub<CahtHub>("/api/v1/hubs/chat");
        return router;
    }
}