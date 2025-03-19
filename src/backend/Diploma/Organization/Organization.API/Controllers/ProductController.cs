using Common.API;
using Common.API.Paged;
using Microsoft.AspNetCore.Mvc;
using Organization.Application.CQRS.Products.Commands;
using Organization.Application.CQRS.Products.Queries;
using Organization.ApplicationContract.Dtos;

namespace Organization.API.Controllers;

public class ProductController : BaseApiController
{
    /// <summary>
    /// Получить товары по идентификатору организации
    /// </summary>
    [HttpPost(nameof(GetProductByOrganization))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<PagedList<ProductDto>>))]
    public async Task<IActionResult> GetProductByOrganization([FromQuery] int organizationId, [FromQuery] PagedRequest request)
    {
        var result = await Mediator.Send(new GetProductsByOrganizationIdQuery(request, organizationId));
        return Ok(ApiResponse<PagedList<ProductDto>>.Success(result));
    }
    
    /// <summary>
    /// Создать товар
    /// </summary>
    [HttpPost(nameof(CreateProduct))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<int>))]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(ApiResponse<int>.Success(result));
    }
}