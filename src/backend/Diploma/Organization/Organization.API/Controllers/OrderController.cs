using Common.API;
using Microsoft.AspNetCore.Mvc;
using Organization.Application.CQRS.Orders.Commands;
using Organization.Application.CQRS.Orders.Queries;
using Organization.ApplicationContract.Dtos;

namespace Organization.API.Controllers;

public class OrderController : BaseApiController
{
    /// <summary>
    /// Получить все закаазы на продажу для организации
    /// </summary>
    [HttpGet(nameof(GetSalesOrder))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<ICollection<OrderDto>>))]
    public async Task<IActionResult> GetSalesOrder([FromQuery] int organizationId)
    {
        var result = await Mediator.Send(new GetSalesOrdersByOrganizationQuery(organizationId));
        return Ok(ApiResponse<ICollection<OrderDto>>.Success(result)); 
    }
    
    /// <summary>
    /// Получить все закаазы на покупку для организации
    /// </summary>
    [HttpGet(nameof(GetSalesOrder))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<int>))]
    public async Task<IActionResult> GetPurchaseOrder([FromQuery] int organizationId)
    {
        var result = await Mediator.Send(new GetPurchaseOrdersByOrganizationQuery(organizationId));
        return Ok(ApiResponse<ICollection<OrderDto>>.Success(result)); 
    }
    
    /// <summary>
    /// Создать заказ
    /// </summary>
    [HttpPost(nameof(CreateOrder))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<int>))]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand commandData)
    {
        var result = await Mediator.Send(commandData);
        return Ok(ApiResponse<int>.Success(result)); 
    }
}