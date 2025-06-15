using AutoMapper;
using Common.Application;
using Microsoft.EntityFrameworkCore;
using Organization.ApplicationContract.Dtos;
using Organization.Infrastructure.Persistance.Context;

namespace Organizaiton.Application.CQRS.Orders.Queries;

/// <summary>
/// Запрос на получине заказа по идентификатору
/// </summary>
public record GetOrderByIdQuery(int OrderId)  : IQuery<OrderDto>;

/// <summary>
/// Хендлер запроса на получине заказа по идентификатору
/// </summary>
internal class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, OrderDto>
{
    private readonly OrganizationDbContext _context;
    private readonly IMapper _mapper;

    public GetOrderByIdQueryHandler(OrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders
                        .AsNoTracking()
                        .Include(x => x.Product)
                        .FirstOrDefaultAsync(x => x.Id == request.OrderId, cancellationToken)
                    ?? throw new ApplicationException("Заказ не найден");

        return _mapper.Map<OrderDto>(order);
    }
}