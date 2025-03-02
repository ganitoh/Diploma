using Common.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Organization.Application.CQRS.Organization.Commands;

namespace Organization.API.Controllers;

[Authorize]
public class OrganizationController : BaseApiController
{
    [HttpPost(nameof(CreateOrganization))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<int>))]
    public async Task<IActionResult> CreateOrganization([FromBody] CreateOrganizationCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(ApiResponse<int>.Success(result));
    }
}