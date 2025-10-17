using Common.Application;
using Common.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notifications.Infrastructure.Persistance.Context;

namespace Notifications.Application.CQRS.Notifications.Commands;

/// <summary>
/// Отметить уведомление как прочитанное
/// </summary>
public record ReadNotificationCommand(int NotificatioId) : ICommand<Unit>;

/// <inheritdoc />
internal class ReadNotificationCommandHandler : ICommandHandler<ReadNotificationCommand, Unit>
{
    private readonly NotificationDbContext _context;

    public ReadNotificationCommandHandler(NotificationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(ReadNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = await _context.Notifications
            .FirstOrDefaultAsync(x=>x.Id == request.NotificatioId, cancellationToken) ?? throw new NotFoundException("Уведомление не найден");

        notification.IsRead = true;
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;  
    }
}