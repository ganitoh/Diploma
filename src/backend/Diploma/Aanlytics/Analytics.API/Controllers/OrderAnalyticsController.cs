using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Analytics.Application.CQRS.Orders.Queries;
using Analytics.ApplicationContract.Dtos;
using Analytics.ApplicationContract.Requests;
using Common.API;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Analytics.API.Controllers;

public class OrderAnalyticsController : BaseApiController
{
    /// <summary>
    /// Получить аналитику по заказам для продающей организации
    /// </summary>
    [HttpPost(nameof(GetOrderAnalyticsBySellerOrganization))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<ICollection<AnalyticsDto>>))]
    public async Task<IActionResult> GetOrderAnalyticsBySellerOrganization([FromBody] GetOrderAnalyticsByStatusRequest request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetOrderAnalyticsBySellerOrganizationQuery(request), cancellationToken);
        return Ok(ApiResponse<ICollection<AnalyticsDto>>.Success(result));
    }
}
