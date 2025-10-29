using AutoMapper;
using Common.Application;
using Common.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notifications.Application.SignalR;
using Notifications.ApplicationContract.Dtos;
using Notifications.Infrastructure.Persistance.Context;

namespace Notifications.Application.CQRS.Notifications.Commands;

/// <summary>
/// Команда для отправки уведомления
/// </summary>
public record SendNotificationCommand(int NotificationId) : ICommand<Unit>;

/// <inheritdoc />
internal class SendNotificationCommandHandler : ICommandHandler<SendNotificationCommand, Unit>
{
    private readonly NotificationDbContext _context;
    private readonly IHubFactory _hubFactory;
    private readonly IMapper _mapper;

    public SendNotificationCommandHandler(IHubFactory hubFactory, NotificationDbContext context, IMapper mapper)
    {
        _hubFactory = hubFactory;
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = await _context.Notifications.FirstOrDefaultAsync(x => x.Id == request.NotificationId, cancellationToken) 
                           ?? throw new NotFoundException("Уведомление не найдено");
        
        await _hubFactory.GetNotificationHub()
            .SendNotification(_mapper.Map<NotificationDto>(notification), cancellationToken);
        
        notification.SentDate = DateTime.UtcNow;
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}