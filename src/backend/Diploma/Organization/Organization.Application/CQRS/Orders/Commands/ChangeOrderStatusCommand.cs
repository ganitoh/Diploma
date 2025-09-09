using Common.Application;
using Common.Application.Exceptions;
using Common.Infrastructure.Kafka;
using Microsoft.EntityFrameworkCore;
using Organization.ApplicationContract.MessageDtos;
using Organization.ApplicationContract.Requests;
using Organization.Infrastructure.Persistance.Context;

namespace Organization.Application.CQRS.Orders.Commands;

/// <summary>
/// Команда на обновление статуса заказа
/// </summary>
public record ChangeOrderStatusCommand(ChangeOrderStatusRequest Data) : ICommand<int>;

/// <inheritdoc/>
internal class ChangeOrderStatusCommandHandler : ICommandHandler<ChangeOrderStatusCommand, int>
{
    private readonly OrganizationDbContext _context;
    private readonly KafkaProducer<ChangeOrderStatusDto> _producer;

    public ChangeOrderStatusCommandHandler(OrganizationDbContext context, KafkaProducer<ChangeOrderStatusDto> producer)
    {
        _context = context;
        _producer = producer;
    }

    public async Task<int> Handle(ChangeOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == request.Data.OrderId, cancellationToken) ??
                    throw new NotFoundException("Заказ не найден");
        
        order.Status = request.Data.Status;
        await _context.SaveChangesAsync(cancellationToken);

        await _producer.ProduceAsync(new ChangeOrderStatusDto
        {
            Id = order.Id,
            Status = order.Status,
            DateTime = DateTime.UtcNow
        }, cancellationToken);
        
        return order.Id;
    }
}