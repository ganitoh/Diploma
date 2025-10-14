using Common.Infrastructure.Kafka;
using MediatR;
using Notifications.Application.CQRS.Notifications.Commands;
using Notifications.ApplicationContract.MessagesDto;

namespace Notifications.API.Consumers;

/// <summary>
/// Обработка уведомлений из kafka
/// </summary>
public class NotificationConsumer(IMediator mediator) : IMessageHandler<CreateNotificationMessage>
{
    public Task HandleAsync(CreateNotificationMessage message, CancellationToken cancellationToken) =>
        mediator.Send(new CreateNotificationCommand(message), cancellationToken);
}