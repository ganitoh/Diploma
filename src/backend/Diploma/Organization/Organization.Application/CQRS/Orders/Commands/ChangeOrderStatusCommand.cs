using Common.Application;
using Common.Application.Exceptions;
using Common.Application.Persistance;
using Common.Domain.Extensions;
using Common.Infrastructure.Kafka;
using Microsoft.EntityFrameworkCore;
using Notifications.ApplicationContract.MessagesDto;
using Notifications.Domain.Enums;
using Organizaiton.Application.Persistance.Repositories;
using Organization.ApplicationContract.Requests;
using Organization.Domain.Models;

namespace Organization.Application.CQRS.Orders.Commands;

/// <summary>
/// Команда на обновление статуса заказа
/// </summary>
public record ChangeOrderStatusCommand(ChangeOrderStatusRequest Data) : ICommand<int>;

/// <inheritdoc/>
internal class ChangeOrderStatusCommandHandler : ICommandHandler<ChangeOrderStatusCommand, int>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly KafkaProducer<CreateNotificationMessage> _producer;

    public ChangeOrderStatusCommandHandler(IOrderRepository orderRepository, KafkaProducer<CreateNotificationMessage> producer, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _producer = producer;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(ChangeOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetQuery()
                        .Include(x=> x.BuyerOrganization) 
                        .ThenInclude(x => x.OrganizationUsers)
                        .FirstOrDefaultAsync(x => x.Id == request.Data.OrderId, cancellationToken) ??
                    throw new NotFoundException("Заказ не найден");
        
        order.Status = request.Data.Status;
        await _unitOfWork.CommitAsync(cancellationToken);

        await SendNotification(order, cancellationToken);
        return order.Id;
    }

    private async Task SendNotification(Order order, CancellationToken cancellationToken)
    {
        await _producer.ProduceAsync(new CreateNotificationMessage
        {
            Title = "Изменен статус заказа",
            UsersIds = order.BuyerOrganization!.OrganizationUsers.Select(x=>x.UserId).ToArray(),
            Text = $"Статус заказа {order.Id} изменен на {order.Status.GetDescription()}",
            Type = NotificationType.Information,
            IsSendImmediately = true
        }, cancellationToken); 
    }
}