using AutoMapper;
using Common.Application;
using MediatR;
using Notifications.ApplicationContract.Requests;
using Notifications.Domain.Models;
using Notifications.Infrastructure.Persistance.Context;

namespace Notifications.Application.CQRS.Notifications.Commands;

/// <summary>
/// Запрос на создание уведомления
/// </summary>
public record CreateNotificationCommand(CreateNotificationRequest RequestData) : IQuery<int>;

/// <inheritdoc />
internal class CreateNotificationCommandHandler : IQueryHandler<CreateNotificationCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly NotificationDbContext _context;

    public CreateNotificationCommandHandler(IMapper mapper, IMediator mediator, NotificationDbContext context)
    {
        _mapper = mapper;
        _mediator = mediator;
        _context = context;
    }

    public async Task<int> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = _mapper.Map<Notification>(request.RequestData);
        notification.CreatedDate = DateTime.UtcNow;
        notification.IsRead = false;
        
        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync(cancellationToken);
        
        if(request.RequestData.IsSendImmediately)
            await _mediator.Send(new SendNotificationCommand(notification.Id), cancellationToken);
        
        return notification.Id;
    }
}