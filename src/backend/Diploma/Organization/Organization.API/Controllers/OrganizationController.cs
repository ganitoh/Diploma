using Common.API;
using Common.API.Paged;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Organizaiton.Application.CQRS.Organizations.Queries;
using Organization.Application.CQRS.Orders.Queries;
using Organization.Application.CQRS.Organizations.Commands;
using Organization.ApplicationContract.Dtos;
using Organization.ApplicationContract.Requests;

namespace Organization.API.Controllers;

public class OrganizationController : BaseApiController
{
    /// <summary>
    /// Получить пагинированый список оргинизаций
    /// </summary>
    [HttpGet(nameof(GetPagedOrganization))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<PagedList<OrganizationDto>>))]
    public async Task<IActionResult> GetPagedOrganization([FromQuery] GetPagedOrganizationsRequest pagedRequest)
    {
        var result = await Mediator.Send(new GetPagedOrganizationQuery(pagedRequest));
        return Ok(ApiResponse<PagedList<OrganizationDto>>.Success(result));
    }
    
    /// <summary>
    /// Получить топ организаций по рейтингу
    /// </summary>
    [HttpGet(nameof(GetTopOrganizationByRating))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<ICollection<ShortOrganizationDto>>))]
    public async Task<IActionResult> GetTopOrganizationByRating([FromQuery] int top)
    {
        var result = await Mediator.Send(new GetTopOrganizationByRatingQuery(top));
        return Ok(ApiResponse<ICollection<ShortOrganizationDto>>.Success(result));
    }
    
    /// <summary>
    /// Получить организацию по идентификатору
    /// </summary>
    [HttpGet(nameof(GetOrganizationById))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<OrganizationDto>))]
    public async Task<IActionResult> GetOrganizationById([FromQuery] int organizationId)
    {
        var result = await Mediator.Send(new GetOrganizationByIdQuery(organizationId));
        return Ok(ApiResponse<OrganizationDto>.Success(result));
    }
    
    /// <summary>
    /// Получить организацию по идентификатору пользователя
    /// </summary>
    [Authorize(Policy = PolicyConst.UserPolicy )]
    [HttpGet(nameof(GetOrganizationByUserId))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<OrganizationDto>))]
    public async Task<IActionResult> GetOrganizationByUserId([FromQuery] string userId)
    {
        var result = await Mediator.Send(new GetOrganizationByUserIdQuery(Guid.Parse(userId)));
        return Ok(ApiResponse<OrganizationDto>.Success(result));
    }
    
    /// <summary>
    /// Получить не подтвержденные организации
    /// </summary>
    [Authorize(Policy = PolicyConst.AdminPolicy )]
    [HttpGet(nameof(GetNotVerifyOrganization))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<ICollection<OrganizationDto>>))]
    public async Task<IActionResult> GetNotVerifyOrganization()
    {
        var result = await Mediator.Send(new GetNotVerifyOrganizationQuery());
        return Ok(ApiResponse<ICollection<OrganizationDto>>.Success(result));
    }
    
    /// <summary>
    /// Создание организации
    /// </summary>
    [Authorize]
    [HttpPost(nameof(CreateOrganization))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<int>))]
    public async Task<IActionResult> CreateOrganization([FromBody] CreateOrganizationRequest requestData)
    {
        var result = await Mediator.Send(new CreateOrganizationCommand(requestData));
        return Ok(ApiResponse<int>.Success(result));
    }
    
    /// <summary>
    /// Обновление данных организации
    /// </summary>
    [Authorize]
    [HttpPut(nameof(UpdateOrganization))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<int>))]
    public async Task<IActionResult> UpdateOrganization([FromBody] UpdateOrganizationDataRequest requestData)
    {
        var result = await Mediator.Send(new UpdateOrganizationDataCommand(requestData));
        return Ok(ApiResponse<int>.Success(result));
    }
    
    /// <summary>
    /// Верификация организации
    /// </summary>
    [Authorize(Policy = PolicyConst.AdminPolicy )]
    [HttpPut(nameof(VerificationOrganization))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<Unit>))]
    public async Task<IActionResult> VerificationOrganization([FromBody] int organizationId)
    {
        var result = await Mediator.Send(new VerifyOrganizationCommand(organizationId));
        return Ok(ApiResponse<Unit>.Success(result));
    }
}