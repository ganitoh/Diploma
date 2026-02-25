using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.API.Paged;
using Common.Application;
using Microsoft.EntityFrameworkCore;
using Organizaiton.Application.Persistance.Repositories;
using Organization.ApplicationContract.Dtos;
using Organization.ApplicationContract.Requests;

namespace Organization.Application.CQRS.Orders.Queries;

/// <summary>
/// Запрос на получение пагинрованного списка заказов для пользователя
/// </summary>
/// <param name="Data"></param>
public record GetPagedOrdersByUserIdQuery(GetOrderByUserRequest Data) : IQuery<PagedList<OrderDto>>;

/// <inheritdoc />
internal class GetPagedOrdersByUserIdQueryHandler : IQueryHandler<GetPagedOrdersByUserIdQuery,  PagedList<OrderDto>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetPagedOrdersByUserIdQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<PagedList<OrderDto>> Handle(GetPagedOrdersByUserIdQuery request, CancellationToken cancellationToken)
    {
        var pagedListOrdersQuery = _orderRepository.GetQuery()
            .AsNoTracking()
            .Include(x => x.Product)
            .Include(x => x.SellerOrganization).ThenInclude(x => x.OrganizationUsers)
            .Include(x => x.BuyerOrganization).ThenInclude(x => x.OrganizationUsers)
            .Where(x => request.Data.IsSellOrders
                ? x.SellerOrganization.OrganizationUsers.First().UserId == request.Data.UserId
                : x.BuyerOrganization.OrganizationUsers.First().UserId == request.Data.UserId);
        
        if (request.Data.Status.HasValue)
            pagedListOrdersQuery = pagedListOrdersQuery.Where(x => x.Status == request.Data.Status);

        var totalCount = await pagedListOrdersQuery.CountAsync(cancellationToken);
        
        var result = await pagedListOrdersQuery
            .PagedQueryable(request.Data.PageNumber, request.Data.PageSize)
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        
        return new PagedList<OrderDto>(result, totalCount);
    }
}