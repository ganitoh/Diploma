using Common.API;
using Common.API.Paged;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Organization.Application.CQRS.Products.Commands;
using Organizaiton.Application.CQRS.Products.Queries;
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
    public async Task<IActionResult> GetPagedProduct([FromQuery] PagedRequest pagedRequest)
    {
        var result = await Mediator.Send(new GetPagedProductsCommand(pagedRequest));
        return Ok(ApiResponse<PagedList<ProductDto>>.Success(result));
    }
    
    /// <summary>
    /// Получить товар по идентификатору
    /// </summary>
    [HttpGet(nameof(GetProductById))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<ProductDto>))]
    public async Task<IActionResult> GetProductById([FromQuery] int productId)
    {
        var result = await Mediator.Send(new GetProductByIdQuery(productId));
        return Ok(ApiResponse<ProductDto>.Success(result));
    }
    
    /// <summary>
    /// Получить топ продоваемых товаров
    /// </summary>
    [HttpGet(nameof(GetTopSellingProducts))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<ICollection<ShortProductDto>>))]
    public async Task<IActionResult> GetTopSellingProducts([FromQuery] int top)
    {
        var result = await Mediator.Send(new GetTopSellingProductsQuery(top));
        return Ok(ApiResponse<ICollection<ShortProductDto>>.Success(result));
    }
    
    /// <summary>
    /// Поиск товаров
    /// </summary>
    [HttpGet(nameof(SearchProducts))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<ICollection<ShortProductDto>>))]
    public async Task<IActionResult> SearchProducts([FromQuery] string searchString)
    {
        var result = await Mediator.Send(new SearchProductsQuery(searchString));
        return Ok(ApiResponse<ICollection<ShortProductDto>>.Success(result));
    }
    
    /// <summary>
    /// Создание товара
    /// </summary>
    [HttpPost(nameof(CreateProduct))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<int>))]
    [Authorize]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest requestData)
    {
        var result = await Mediator.Send(new CreateProductCommand(requestData));
        return Ok(ApiResponse<int>.Success(result));
    }
    
    /// <summary>
    /// Обновление товара
    /// </summary>
    [HttpPut(nameof(UpdateProduct))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<int>))]
    [Authorize]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductRequest requestData)
    {
        var result = await Mediator.Send(new UpdateProductCommand(requestData));
        return Ok(ApiResponse<int>.Success(result));
    }
}