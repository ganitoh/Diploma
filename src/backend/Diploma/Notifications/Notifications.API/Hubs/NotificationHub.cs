using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Notifications.Application.SignalR;
using Notifications.ApplicationContract.Dtos;

namespace Notifications.API.Hubs;

public class NotificationHub : Hub<INotificationClient>, INotificationHub
{
    private readonly IMemoryCache _cache;
    private readonly ILogger<NotificationHub> _logger;
    private readonly IMediator _mediator;

    public NotificationHub(IMemoryCache cache, ILogger<NotificationHub> logger, IMediator mediator)
    {
        _cache = cache;
        _logger = logger;
        _mediator = mediator;
    }
    
    /// <inheritdoc />
    public override async Task OnConnectedAsync()
    {
        var userId = Context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        
        if (userId != null)
        { 
            _logger.LogInformation("Присоединился пользователь {userId}", userId);
            _cache.Set(userId,  Context.ConnectionId);
        } 
        await base.OnConnectedAsync();
    }

    /// <inheritdoc />
    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var userId = Context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userId != null)
        { 
            _cache.Remove(userId);
        } 
        await base.OnDisconnectedAsync(exception);
    }

    public Task SendNotification(NotificationDto request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Уведомление отправлено {notificationId}", request.Id);
        throw new NotImplementedException();
    }
}