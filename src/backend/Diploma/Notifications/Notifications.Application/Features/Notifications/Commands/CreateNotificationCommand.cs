using AutoMapper;
using Common.Application;
using Common.Application.Persistance;
using MediatR;
using Notifications.Application.Common.Persistance.Repositories;
using Notifications.ApplicationContract.Requests;
using Notifications.Domain.Models;

namespace Notifications.Application.CQRS.Notifications.Commands;

public record CreateNotificationCommand(CreateNotificationRequest RequestData) : IQuery<int>;

internal class CreateNotificationCommandHandler : IQueryHandler<CreateNotificationCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly INotificationRepository _notificationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateNotificationCommandHandler(IMapper mapper, IMediator mediator, INotificationRepository notificationRepository, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _mediator = mediator;
        _notificationRepository = notificationRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task<int> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = _mapper.Map<Notification>(request.RequestData);
        notification.CreatedDate = DateTime.UtcNow;
        notification.IsRead = false;
        
        _notificationRepository.Create(notification);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        if(request.RequestData.IsSendImmediately)
            await _mediator.Send(new SendNotificationCommand(notification.Id), cancellationToken);
        
        return notification.Id;
    }
}