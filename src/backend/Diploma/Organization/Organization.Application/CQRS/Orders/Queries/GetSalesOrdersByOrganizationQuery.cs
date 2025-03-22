using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Application;
using Microsoft.EntityFrameworkCore;
using Organization.Application.Common.Persistance;
using Organization.ApplicationContract.Dtos;

namespace Organization.Application.CQRS.Orders.Queries;

/// <summary>
/// Получить заказы на продажу
/// </summary>
public record class GetSalesOrdersByOrganizationQuery(int OrganizationId) : IQuery<ICollection<OrderDto>>;

/// <summary>
/// Хендлер для получения заказов на продажу
/// </summary>
internal class GetSalesOrdersByOrganizationQueryHandler : IQueryHandler<GetSalesOrdersByOrganizationQuery, ICollection<OrderDto>>
{
    private readonly IMapper _mapper; 
    private readonly IReadOnlyOrganizationDbContext  _dbContext;

    public GetSalesOrdersByOrganizationQueryHandler(IMapper mapper, IReadOnlyOrganizationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<ICollection<OrderDto>> Handle(GetSalesOrdersByOrganizationQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Orders.Where(x => x.SellerOrganizationId == request.OrganizationId)
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
    }
}