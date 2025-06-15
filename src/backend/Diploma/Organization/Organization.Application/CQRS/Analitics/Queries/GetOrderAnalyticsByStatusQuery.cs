using Common.Application;
using Common.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using Organizaiton.Application.Extensions;
using Organization.ApplicationContract.AnaliticsDtos;
using Organization.ApplicationContract.Requests.Analytics;
using Organization.Domain.Enums;
using Organization.Domain.Models;
using Organization.Infrastructure.Persistance.Context;

namespace Organizaiton.Application.CQRS.Analitics.Queries;

/// <summary>
/// Команад для получения аналитических данных для заказов по статусам
/// </summary>
public record GetOrderAnalyticsByStatusQuery(GetOrderAnalyticsByStatusRequest Data) : IQuery<ICollection<AnalyticsDto>>;

/// <inheritdoc />
internal class GetOrderAnalyticsByStatusQueryHandler  : IQueryHandler<GetOrderAnalyticsByStatusQuery, ICollection<AnalyticsDto>>
{
    private readonly OrganizationDbContext _context;

    public GetOrderAnalyticsByStatusQueryHandler(OrganizationDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<AnalyticsDto>> Handle(GetOrderAnalyticsByStatusQuery request, CancellationToken cancellationToken)
    {
        var orders = _context.Orders
            .AsNoTracking()
            .OrderBy(x => x.CreateDate)
            .Where(x =>  request.Data.Statuses.Contains(x.Status) && x.SellerOrganizationId == request.Data.EntityId)
            .FilterByDateAnalytics(request.Data.StartDate, request.Data.EndDate);
        
        var groupedOrders = await orders
            .GroupBy(o => o.Status)
            .Select(g => new AnalyticsDto
            {
                Name = g.Key.GetDescription(),
                Value = g.Count()
                                            
            })
            .ToListAsync(cancellationToken);
        
        return groupedOrders;
    }
}