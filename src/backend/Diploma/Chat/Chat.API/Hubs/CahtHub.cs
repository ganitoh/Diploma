﻿using System.Security.Claims;
using AutoMapper;
using Chat.Application.CQRS.Chats.Queries;
using Chat.Application.CQRS.Messages.Commands;
using Chat.Application.SignalR;
using Chat.ApplicationContract.Dtos;
using Chat.ApplicationContract.Requests;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;

namespace Chat.API.Hubs;

public class CahtHub : Hub<IChatClient>
{
    private readonly IMemoryCache _cache;
    private readonly IMediator _mediator;
    private readonly ILogger<CahtHub> _logger;

    public CahtHub(IMemoryCache cache, IMediator mediator, ILogger<CahtHub> logger)
    {
        _cache = cache;
        _mediator = mediator;
        _logger = logger;
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

    
    /// <summary>
    /// Отправить сообщение
    /// </summary>
    public async Task SendMessage(CreateMessageRequest request)
    {
        var userId = Context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        request.UserId = Guid.Parse(userId);

        var response = await _mediator.Send(new CreateMessageCommand(request));
        var chat = await _mediator.Send(new GetChatQuery(request.OrderId));
        
        if (_cache.TryGetValue(Guid.Parse(userId) == chat.FirstUserId ? chat.SecondUserId.ToString() : chat.FirstUserId.ToString(), out var userConnection))
        {
            _logger.LogInformation("Сообщение отправлено пользователю {userId} с коннектом {userConnection}", userId, userConnection);
            await Clients.Client(userConnection.ToString()).ReceiveMessagesAsync(new MessageDto
            {
                Id = response,
                UserId = Guid.Parse(userId),
                Text = request.Text,
                CreatedDatetime = DateTime.UtcNow
            });
        }

    }
}