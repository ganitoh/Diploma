using Common.API;
using Common.API.Paged;
using Microsoft.AspNetCore.Mvc;
using Organizaiton.Application.CQRS.Organizations.Queries;
using Organization.Application.CQRS.Organizations.Commands;
using Organization.ApplicationContract.Dtos;
using Organization.ApplicationContract.Requests;

namespace Organization.API.Controllers;

public class OrganizationController : BaseApiController
{
    
    /// <summary>
    /// Получить пагинированый список оргинизация
    /// </summary>
    [HttpGet(nameof(GetPagedOrganization))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<PagedList<OrganizationDto>>))]
    public async Task<IActionResult> GetPagedOrganization([FromQuery] PagedRequest pagedRequest)
    {
        var result = await Mediator.Send(new GetPagedOrganizationQuery(pagedRequest));
        return Ok(ApiResponse<PagedList<OrganizationDto>>.Success(result));
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
    /// Создание организации
    /// </summary>
    [HttpPost(nameof(CreateOrganization))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<int>))]
    public async Task<IActionResult> CreateOrganization([FromBody] CreateOrganizationRequest requestData)
    {
        var result = await Mediator.Send(new CreateOrganizationCommand(requestData));
        return Ok(ApiResponse<int>.Success(result));
    }
}