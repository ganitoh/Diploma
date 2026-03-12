using Common.Infrastructure.Kafka;
using MediatR;
using Notifications.Application.CQRS.Notifications.Commands;
using Notifications.ApplicationContract.Requests;
using Notifications.Domain.Enums;
using Organization.ApplicationContract.Messages;

namespace Notifications.API.Consumers;

/// <summary>
/// Обработка уведомлений из kafka
/// </summary>
public class NotificationConsumer(IMediator mediator) : IMessageHandler<CreateOrderMessage>
{
    private const string Template = "Создан заказ №{0} {1}"; 
    private const string Title = "Создан заказ"; 

    public async Task HandleAsync(CreateOrderMessage message, CancellationToken cancellationToken)
    {
        var request = new CreateNotificationRequest(
            message.UserIds,
            NotificationType.Information,
            false,Title,
            string.Format(Template, message.OrderId, message.CreateAtDate),
            true);
        
         await mediator.Send(new CreateNotificationCommand(request), cancellationToken);
    }
}