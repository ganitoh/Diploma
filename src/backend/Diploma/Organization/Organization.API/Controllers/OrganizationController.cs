using Common.API;
using Common.API.Paged;
using Microsoft.AspNetCore.Mvc;
using Organization.Application.CQRS.Organization.Commands;
using Organization.Application.CQRS.Organization.Queries;
using Organization.ApplicationContract.Dtos;

namespace Organization.API.Controllers;

public class OrganizationController : BaseApiController
{
    
    /// <summary>
    /// Получить список всех организаций
    /// </summary>
    [HttpPost(nameof(GetAll))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<int>))]
    public async Task<IActionResult> GetAll([FromQuery] PagedRequest request)
    {
        var result = await Mediator.Send(new GetAllOrganizationsQuery(request));
        return Ok(ApiResponse<PagedList<OrganizationDto>>.Success(result));
    }
    
    /// <summary>
    /// Создать организацию
    /// </summary>
    [HttpPost(nameof(CreateOrganization))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<int>))]
    public async Task<IActionResult> CreateOrganization([FromBody] CreateOrganizationCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(ApiResponse<int>.Success(result));
    }
}