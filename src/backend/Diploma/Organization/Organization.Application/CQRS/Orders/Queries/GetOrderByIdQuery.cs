using AutoMapper;
using Common.Application;
using Microsoft.EntityFrameworkCore;
using Organization.Application.Commnon.Persistance;
using Organization.ApplicationContract.Dtos;

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
    private readonly IReadonlyOrganizationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetOrderByIdQueryHandler(IReadonlyOrganizationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == request.OrderId, cancellationToken)
                    ?? throw new ApplicationException("Заказ не найден");

        return _mapper.Map<OrderDto>(order);
    }
}