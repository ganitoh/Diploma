using Common.API;
using Common.API.Paged;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Organization.Application.Features.Products.Commands;
using Organization.Application.Features.Products.Queries;
using Organization.ApplicationContract.Dtos;
using Organization.ApplicationContract.Requests;

namespace Organization.API.Controllers;

public class ProductController : BaseApiController
{
    /// <summary>
    /// Получить пагинированый список товаров
    /// </summary>
    [HttpGet(nameof(GetPagedProduct))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<PagedList<ProductDto>>))]
    public async Task<IActionResult> GetPagedProduct([FromQuery] GetPagedProductsRequest pagedRequest, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetPagedProductsCommand(pagedRequest), cancellationToken);
        return Ok(ApiResponse<PagedList<ProductDto>>.Success(result));
    }
    
    /// <summary>
    /// Получить товар по идентификатору
    /// </summary>
    [HttpGet(nameof(GetProductById))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<ProductDto>))]
    public async Task<IActionResult> GetProductById([FromQuery] int productId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetProductByIdQuery(productId), cancellationToken);
        return Ok(ApiResponse<ProductDto>.Success(result));
    }
    
    /// <summary>
    /// Получить топ продоваемых товаров
    /// </summary>
    [HttpGet(nameof(GetTopSellingProducts))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<ICollection<ShortProductDto>>))]
    public async Task<IActionResult> GetTopSellingProducts([FromQuery] int top, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetTopSellingProductsQuery(top), cancellationToken);
        return Ok(ApiResponse<ICollection<ShortProductDto>>.Success(result));
    }
    
    /// <summary>
    /// Поиск товаров
    /// </summary>
    [HttpGet(nameof(SearchProducts))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<ICollection<ShortProductDto>>))]
    public async Task<IActionResult> SearchProducts([FromQuery] string searchString, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new SearchProductsQuery(searchString), cancellationToken);
        return Ok(ApiResponse<ICollection<ShortProductDto>>.Success(result));
    }
    
    /// <summary>
    /// Получить товары для организации (сокращенные данные)
    /// </summary>
    [HttpGet(nameof(GetShortProductByOrganization))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<ICollection<ShortProductDto>>))]
    public async Task<IActionResult> GetShortProductByOrganization([FromQuery] int organizationId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetProductsByOrganizationQuery(organizationId), cancellationToken);
        return Ok(ApiResponse<ICollection<ShortProductDto>>.Success(result));
    }
    
    /// <summary>
    /// Создание товара
    /// </summary>
    [HttpPost(nameof(CreateProduct))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<int>))]
    [Authorize]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest requestData, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new CreateProductCommand(requestData), cancellationToken);
        return Ok(ApiResponse<int>.Success(result));
    }
    
    /// <summary>
    /// Обновление товара
    /// </summary>
    [HttpPut(nameof(UpdateProduct))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<int>))]
    [Authorize]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductRequest requestData, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new UpdateProductCommand(requestData), cancellationToken);
        return Ok(ApiResponse<int>.Success(result));
    }
    
    /// <summary>
    /// Удаление товара
    /// </summary>
    [HttpDelete(nameof(DeleteProduct))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<Unit>))]
    [Authorize]
    public async Task<IActionResult> DeleteProduct([FromBody] int[] Ids, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new DeleteProductCommand(Ids), cancellationToken);
        return Ok(ApiResponse<Unit>.Success(result));
    }
}