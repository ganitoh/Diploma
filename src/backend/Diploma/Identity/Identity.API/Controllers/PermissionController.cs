using Common.API;
using Identity.ApplicatinContract.Dtos;
using Identity.Application.CQRS.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers;

public class PermissionController : BaseApiController
{
    /// <summary>
    /// Получить все разрешения
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<ICollection<PermissionDto>>))]
    public async Task<IActionResult> GetPermissions()
    {
        var result = await Mediator.Send(new GetPermissionsQuery());
        return Ok(ApiResponse<ICollection<PermissionDto>>.Success(result));
    }
}