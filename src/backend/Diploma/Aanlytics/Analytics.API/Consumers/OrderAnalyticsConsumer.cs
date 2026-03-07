using Analytics.Application.CQRS.OrderItems.Commands;
using Analytics.Application.CQRS.Orders.Commands;
using Analytics.ApplicationContract.Requests;
using Common.Infrastructure.Kafka;
using MediatR;
using Organization.ApplicationContract.Messages;

namespace Analytics.API.Consumers;

public class OrderAnalyticsConsumer(IMediator mediator) : IMessageHandler<CreateOrderMessage>
{
    public async Task HandleAsync(CreateOrderMessage message, CancellationToken cancellationToken)
    {
        await mediator.Send(new CreateOrderAnalyticsCommand(
            new CreateOrderAnalyticsRequest(
                message.Status,
                message.OrderId,
                message.TotalPrice,
                message.SellerOrganizationId,
                message.BuyerOrganizationId,
                message.CreateAtDate,
                message.Items.ToList())), cancellationToken);
    }
}