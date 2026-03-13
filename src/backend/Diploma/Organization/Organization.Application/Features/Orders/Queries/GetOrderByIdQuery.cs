using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Application;
using Microsoft.EntityFrameworkCore;
using Organizaiton.Application.Common.Persistance;
using Organization.ApplicationContract.Dtos;

namespace Organization.Application.Features.Orders.Queries;

/// <summary>
/// Запрос на получине заказа по идентификатору
/// </summary>
public record GetOrderByIdQuery(int OrderId)  : IQuery<OrderDto>;

/// <summary>
/// Хендлер запроса на получине заказа по идентификатору
/// </summary>
internal class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, OrderDto>
{
    private readonly IReadOnlyOrganizationDbContext _context;
    private readonly IMapper _mapper;

    public GetOrderByIdQueryHandler(IReadOnlyOrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await  _context.Orders
                        .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync(x => x.Id == request.OrderId, cancellationToken)
                    ?? throw new ApplicationException("Заказ не найден");

        return order;
    }
}