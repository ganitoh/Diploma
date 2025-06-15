using Common.Application;
using Microsoft.EntityFrameworkCore;
using Organizaiton.Application.Extensions;
using Organization.ApplicationContract.AnaliticsDtos;
using Organization.ApplicationContract.Requests.Analytics;
using Organization.Domain.Models;
using Organization.Infrastructure.Persistance.Context;

namespace Organizaiton.Application.CQRS.Analitics.Queries;

/// <summary>
/// Запрос на получение аналитических данных по заказам на продажу для организации
/// </summary>
public record GetSellOrderAnalyticsByOrganizationQuery(GetAnalyticsRequest Data) :  IQuery<ICollection<AnalyticsDto>>;

/// <inheritdoc />
internal class GetSellOrderAnalyticsByOrganizationQueryHandler : IQueryHandler<GetSellOrderAnalyticsByOrganizationQuery,
    ICollection<AnalyticsDto>>
{
    private readonly OrganizationDbContext _context;

    public GetSellOrderAnalyticsByOrganizationQueryHandler(OrganizationDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<AnalyticsDto>> Handle(GetSellOrderAnalyticsByOrganizationQuery request, CancellationToken cancellationToken)
    {
        var orders = _context.Orders
            .AsNoTracking()
            .OrderBy(x => x.CreateDate)
            .Where(x => x.SellerOrganizationId == request.Data.EntityId)
            .FilterByDateAnalytics(request.Data.StartDate, request.Data.EndDate);
        
        var groupedOrders = await orders
            .GroupBy(o => o.CreateDate.Date)
            .Select(g => new AnalyticsDto
            {
                Name = g.Key.ToShortDateString(),
                Value = g.Count()
                                            
            })
            .ToListAsync(cancellationToken);
        
        return groupedOrders;
    }
}