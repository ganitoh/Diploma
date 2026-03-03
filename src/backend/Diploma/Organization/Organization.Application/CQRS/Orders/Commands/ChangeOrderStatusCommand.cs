using Common.Application;
using Common.Application.Exceptions;
using Common.Application.Persistance;
using Common.Domain.Extensions;
using Common.Infrastructure.Kafka;
using Microsoft.EntityFrameworkCore;
using Notifications.ApplicationContract.MessagesDto;
using Notifications.Domain.Enums;
using Organizaiton.Application.Common.Persistance;
using Organization.ApplicationContract.Requests;
using Organization.Domain.Enums;
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
    private readonly IReadOnlyOrganizationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly KafkaProducer<CreateNotificationMessage> _producer;

    public ChangeOrderStatusCommandHandler(
        IOrderRepository orderRepository,
        IReadOnlyOrganizationDbContext context,
        IUnitOfWork unitOfWork,
        KafkaProducer<CreateNotificationMessage> producer)
    {
        _orderRepository = orderRepository;
        _context = context;
        _unitOfWork = unitOfWork;
        _producer = producer;
    }

    public async Task<int> Handle(ChangeOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.Data.OrderId) 
                    ?? throw new NotFoundException("Заказ не найден");

        switch (request.Data.Status)
                 {
                     case OrderStatus.Close:
                         order.Closed();
                         break;
                     case OrderStatus.Created:
                         order.Created();
                         break;
                     case OrderStatus.InDelivery:
                         order.Delivery();
                         break;
                     case OrderStatus.Collected:
                         order.Collected();
                         break;
                 }
        
        await _unitOfWork.CommitAsync(cancellationToken);

        await SendNotification(order, cancellationToken);
        return order.Id;
    }

    private async Task SendNotification(Order order, CancellationToken cancellationToken)
    {
        var userIds = await _context.Organizations
            .Include(x => x.OrganizationUsers)
            .Where(x => x.Id == order.BuyerOrganizationId)
            .SelectMany(x => x.OrganizationUsers)
            .Select(x => x.UserId)
            .ToArrayAsync(cancellationToken);
            
        await _producer.ProduceAsync(new CreateNotificationMessage
        {
            Title = "Изменен статус заказа",
            UsersIds = userIds,
            Text = $"Статус заказа {order.Id} изменен на {order.Status.GetDescription()}",
            Type = NotificationType.Information,
            IsSendImmediately = true
        }, cancellationToken); 
    }
}