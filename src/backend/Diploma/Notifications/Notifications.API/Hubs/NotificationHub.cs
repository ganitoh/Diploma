using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Notifications.Application.SignalR;
using Notifications.ApplicationContract.Dtos;

namespace Notifications.API.Hubs;

public class NotificationHub : Hub<INotificationClient>, INotificationHub
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<NotificationHub> _logger;
    private readonly IMediator _mediator;

    public NotificationHub(IDistributedCache cache, ILogger<NotificationHub> logger, IMediator mediator)
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
            await _cache.SetStringAsync(userId,  Context.ConnectionId);
        } 
        await base.OnConnectedAsync();
    }

    /// <inheritdoc />
    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var userId = Context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        
        if (userId != null)
            await _cache.RemoveAsync(userId);
         
        await base.OnDisconnectedAsync(exception);
    }

    /// <inheritdoc/>
    public async Task SendNotification(NotificationDto request, CancellationToken cancellationToken = default)
    {
        var userConnection = await _cache.GetStringAsync(request.UserId.ToString(), cancellationToken);
        if (userConnection is not null)
        {
            await Clients.Client(userConnection).ReceiveNotifications(request);
            _logger.LogInformation("Отправлено уведомление пользователю {userId} с коннектом {userConnection}", request.UserId, userConnection);
        }
    }
}