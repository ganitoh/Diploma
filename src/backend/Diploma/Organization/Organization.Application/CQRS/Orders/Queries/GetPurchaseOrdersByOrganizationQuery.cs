using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Application;
using Common.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Organization.Application.Common.Persistance;
using Organization.ApplicationContract.Dtos;

namespace Organization.Application.CQRS.Orders.Queries;

/// <summary>
/// Получить закзаы на покупку для организации
/// </summary>
public record class GetPurchaseOrdersByOrganizationQuery(int OrganizationId) : IQuery<ICollection<OrderDto>>;

internal class GetPurchaseOrdersByOrganizationQueryHandler : IQueryHandler<GetPurchaseOrdersByOrganizationQuery, ICollection<OrderDto>>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyOrganizationDbContext  _dbContext;

    public GetPurchaseOrdersByOrganizationQueryHandler(IMapper mapper, IReadOnlyOrganizationDbContext context)
    {
        _mapper = mapper;
        _dbContext = context;
    }

    public async Task<ICollection<OrderDto>> Handle(GetPurchaseOrdersByOrganizationQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Orders.Where(x => x.BuyerOrganizationId == request.OrganizationId)
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
    }
}