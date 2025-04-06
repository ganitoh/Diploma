using Common.API;
using Common.API.Paged;
using Microsoft.AspNetCore.Mvc;
using Organization.Application.CQRS.Orders.Commands;
using Organization.Application.CQRS.Orders.Queries;
using Organization.ApplicationContract.Dtos;
using Organization.ApplicationContract.Requests;

namespace Organization.API.Controllers;

public class OrderContorller : BaseApiController
{
    /// <summary>
    /// Получить заказы на покупку для организации
    /// </summary>
    [HttpGet(nameof(GetBuyOrderByOrganizaiton))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<PagedList<OrderDto>>))]
    public async Task<IActionResult> GetBuyOrderByOrganizaiton([FromQuery] PagedRequest pagedRequest, [FromQuery] int organizationId)
    {
        var result = await Mediator.Send(new GetBuyOrdersByOrganizationQuery(pagedRequest, organizationId));
        return Ok(ApiResponse<PagedList<OrderDto>>.Success(result));
    }
    
    /// <summary>
    /// Получить заказы на продужу для организации
    /// </summary>
    [HttpGet(nameof(GetSellOrderByOrganizaiton))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<PagedList<OrderDto>>))]
    public async Task<IActionResult> GetSellOrderByOrganizaiton([FromQuery] PagedRequest pagedRequest, [FromQuery] int organizationId)
    {
        var result = await Mediator.Send(new GetSellOrdersByOrganizationQuery(pagedRequest, organizationId));
        return Ok(ApiResponse<PagedList<OrderDto>>.Success(result));
    }
    
    /// <summary>
    /// Создание заказа
    /// </summary>
    [HttpPost(nameof(CreateOrder))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<int>))]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest requestData)
    {
        var result = await Mediator.Send(new CreateOrderCommand(requestData));
        return Ok(ApiResponse<int>.Success(result));
    }
}