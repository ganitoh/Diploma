using Common.Application;
using Common.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Organization.ApplicationContract.Requests;
using Organization.Domain.Enums;
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

    public ChangeOrderStatusCommandHandler(OrganizationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(ChangeOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == request.Data.OrderId, cancellationToken) ??
                    throw new NotFoundException("Заказ не найден");
        
        order.Status = request.Data.Status;
        await _context.SaveChangesAsync(cancellationToken);
        return order.Id;
    }
}