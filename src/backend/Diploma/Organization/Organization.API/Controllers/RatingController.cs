using Common.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Organizaiton.Application.CQRS.Ratings.Commands;
using Organizaiton.Application.CQRS.Ratings.Queries;
using Organization.ApplicationContract.Dtos;
using Organization.ApplicationContract.Requests;

namespace Organization.API.Controllers;

public class RatingController : BaseApiController
{
    /// <summary>
    /// Получить все отзывы для сущности
    /// </summary>
    [HttpGet(nameof(GetRatingForEntity))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<RatingDto>))]
    public async Task<IActionResult> GetRatingForEntity([FromQuery] int entityId, [FromQuery] bool isProduct)
    {
        var result = await Mediator.Send(new GetRatingsQuery(entityId, isProduct));
        return Ok(ApiResponse<RatingDto>.Success(result));
    }
    
    /// <summary>
    /// Создание товара
    /// </summary>
    [HttpPost(nameof(CreateRating))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<int>))]
    [Authorize]
    public async Task<IActionResult> CreateRating([FromBody] CreateRatingRequest requestData)
    {
        var result = await Mediator.Send(new CreateRatingCommand(requestData, User));
        return Ok(ApiResponse<int>.Success(result));
    }
}