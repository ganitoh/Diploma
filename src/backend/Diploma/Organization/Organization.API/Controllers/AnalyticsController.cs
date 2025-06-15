using Common.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Organizaiton.Application.CQRS.Analitics.Queries;
using Organization.ApplicationContract.AnaliticsDtos;
using Organization.ApplicationContract.Requests.Analytics;

namespace Organization.API.Controllers;

/// <summary>
/// Аналитика
/// </summary>
public class AnalyticsController : BaseApiController
{
    /// <summary>
    /// Получить аналитические данные по заказам на продажу для организации
    /// </summary>
    [Authorize]
    [HttpGet(nameof(GetSellOrderAnalytics))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<ICollection<AnalyticsDto>>))]
    public async Task<IActionResult> GetSellOrderAnalytics([FromQuery] GetAnalyticsRequest request)
    {
        var result = await Mediator.Send(new GetSellOrderAnalyticsByOrganizationQuery(request));
        return Ok(ApiResponse<ICollection<AnalyticsDto>>.Success(result));
    }
    
    /// <summary>
    /// Получить аналитические данные по заказам на продажу для организации по статусам
    /// </summary>
    [Authorize]
    [HttpPost(nameof(GetSellOrderAnalyticsByStatus))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<ICollection<AnalyticsDto>>))]
    public async Task<IActionResult> GetSellOrderAnalyticsByStatus([FromBody] GetOrderAnalyticsByStatusRequest request)
    {
        var result = await Mediator.Send(new GetOrderAnalyticsByStatusQuery(request));
        return Ok(ApiResponse<ICollection<AnalyticsDto>>.Success(result));
    }
    
    /// <summary>
    /// Получить аналитические данные по товарам которые на заказах
    /// </summary>
    [Authorize]
    [HttpGet(nameof(GetProductAnalytics))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<ICollection<AnalyticsDto>>))]
    public async Task<IActionResult> GetProductAnalytics([FromQuery] GetAnalyticsRequest request)
    {
        var result = await Mediator.Send(new GetSellOrderAnalyticsByOrganizationQuery(request));
        return Ok(ApiResponse<ICollection<AnalyticsDto>>.Success(result));
    }
}