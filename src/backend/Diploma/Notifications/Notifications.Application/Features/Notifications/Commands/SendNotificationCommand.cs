using AutoMapper;
using Common.Application;
using Common.Application.Exceptions;
using Common.Application.Persistance;
using MediatR;
using Notifications.Application.Common.Persistance.Repositories;
using Notifications.Application.SignalR;
using Notifications.ApplicationContract.Dtos;

namespace Notifications.Application.CQRS.Notifications.Commands;

/// <summary>
/// Команда для отправки уведомления
/// </summary>
public record SendNotificationCommand(int NotificationId) : ICommand<Unit>;

/// <inheritdoc />
internal class SendNotificationCommandHandler : ICommandHandler<SendNotificationCommand, Unit>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHubFactory _hubFactory;
    private readonly IMapper _mapper;

    public SendNotificationCommandHandler(
        INotificationRepository notificationRepository,
        IUnitOfWork unitOfWork,
        IHubFactory hubFactory,
        IMapper mapper)
    {
        _notificationRepository = notificationRepository;
        _unitOfWork = unitOfWork;
        _hubFactory = hubFactory;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = await _notificationRepository.GetByIdAsync(request.NotificationId);
        
        if (notification is null)
            throw new NotFoundException("Уведомление не найдено");
        
        await _hubFactory.GetNotificationHub()
            .SendNotification(_mapper.Map<NotificationDto>(notification), cancellationToken);
        
        notification.SentDate = DateTime.UtcNow;
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return Unit.Value;
    }
}