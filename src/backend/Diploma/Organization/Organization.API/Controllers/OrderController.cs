using Common.API;
using Common.API.Paged;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Organizaiton.Application.CQRS.Orders.Queries;
using Organization.Application.CQRS.Orders.Commands;
using Organization.Application.CQRS.Orders.Queries;
using Organization.ApplicationContract.Dtos;
using Organization.ApplicationContract.Requests;

namespace Organization.API.Controllers;

public class OrderController : BaseApiController
{
    /// <summary>
    /// Получить заказ по идентификатору
    /// </summary>
    [HttpGet(nameof(GetOrderById))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<OrderDto>))]
    public async Task<IActionResult> GetOrderById([FromQuery] int organizationId)
    {
        var result = await Mediator.Send(new GetOrderByIdQuery(organizationId));
        return Ok(ApiResponse<OrderDto>.Success(result));
    }
    
    /// <summary>
    /// Получить пагинированный список заказов
    /// </summary>
    [Authorize]
    [HttpGet(nameof(GetPagedOrderByUserId))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<PagedList<OrderDto>>))]
    public async Task<IActionResult> GetPagedOrderByUserId([FromQuery] GetOrderByUserRequest request)
    {
        request.UserId = GetUserId();
        var result = await Mediator.Send(new GetPagedOrdersByUserIdQuery(request));
        return Ok(ApiResponse<PagedList<OrderDto>>.Success(result));
    }

    /// <summary>
    /// Получить заказы на покупку для организации
    /// </summary>
    [HttpGet(nameof(GetBuyOrderByOrganization))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<PagedList<OrderDto>>))]
    public async Task<IActionResult> GetBuyOrderByOrganization(
        [FromQuery] PagedRequest pagedRequest,
        [FromQuery] int organizationId,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetBuyOrdersByOrganizationQuery(pagedRequest, organizationId), cancellationToken);
        return Ok(ApiResponse<PagedList<OrderDto>>.Success(result));
    }

    /// <summary>
    /// Получить заказы на продужу для организации
    /// </summary>
    [HttpGet(nameof(GetSellOrderByOrganization))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<PagedList<OrderDto>>))]
    public async Task<IActionResult> GetSellOrderByOrganization([FromQuery] PagedRequest pagedRequest,
        [FromQuery] int organizationId)
    {
        var result = await Mediator.Send(new GetSellOrdersByOrganizationQuery(pagedRequest, organizationId));
        return Ok(ApiResponse<PagedList<OrderDto>>.Success(result));
    }

    /// <summary>
    /// Получить накладную по заказу
    /// </summary>
    [Authorize]
    [HttpGet(nameof(GetInvoiceForOrder))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<FileDto>))]
    public async Task<IActionResult> GetInvoiceForOrder([FromQuery] int orderId)
    {
        var result = await Mediator.Send(new GetInvoiceForOrderQuery(orderId));
        return Ok(ApiResponse<FileDto>.Success(result));
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

    /// <summary>
    /// Изменение статуса заказа
    /// </summary>
    [HttpPut(nameof(ChangeOrderStatus))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<int>))]
    public async Task<IActionResult> ChangeOrderStatus([FromBody] ChangeOrderStatusRequest requestData)
    {
        var result = await Mediator.Send(new ChangeOrderStatusCommand(requestData));
        return Ok(ApiResponse<int>.Success(result));
    }
}