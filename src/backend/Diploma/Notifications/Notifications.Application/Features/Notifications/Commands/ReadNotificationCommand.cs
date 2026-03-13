using Common.Application;
using Common.Application.Exceptions;
using Common.Application.Persistance;
using MediatR;
using Notifications.Application.Common.Persistance.Repositories;

namespace Notifications.Application.CQRS.Notifications.Commands;


public record ReadNotificationCommand(int NotificatioId) : ICommand<Unit>;

/// <inheritdoc />
internal class ReadNotificationCommandHandler : ICommandHandler<ReadNotificationCommand, Unit>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ReadNotificationCommandHandler(INotificationRepository notificationRepository, IUnitOfWork unitOfWork)
    {
        _notificationRepository = notificationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(ReadNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = await _notificationRepository.GetByIdAsync(request.NotificatioId);
        
        if (notification is null)
            throw new NotFoundException("Уведомление не найдено");

        notification.IsRead = true;
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return Unit.Value;  
    }
}