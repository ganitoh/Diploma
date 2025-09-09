using Common.API;
using Identity.ApplicatinContract.Dtos;
using Identity.ApplicatinContract.Requests;
using Identity.Application.CQRS.Roles.Commands;
using Identity.Application.CQRS.Roles.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers;

public class RoleController : BaseApiController
{
    /// <summary>
    /// Получить все роли
    /// </summary>
    /// <returns></returns>
    [HttpGet(nameof(GetRoles))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<ICollection<RoleDto>>))]
    public async Task<IActionResult> GetRoles()
    {
        var result = await Mediator.Send(new GetRolesQuery());
        return Ok(ApiResponse<ICollection<RoleDto>>.Success(result));
    }
    
    /// <summary>
    /// Получить роль по названию
    /// </summary>
    [HttpGet(nameof(GetRoleByName))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<RoleDto>))]
    public async Task<IActionResult> GetRoleByName([FromQuery] string roleName)
    {
        var result = await Mediator.Send(new GetRoleByNameQuery(roleName));
        return Ok(ApiResponse<RoleDto>.Success(result));
    }
    
    /// <summary>
    /// Проверить роль
    /// </summary>
    [HttpGet(nameof(CheckRole))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
    public async Task<IActionResult> CheckRole([FromQuery] string roleName)
    {
        var result = await Mediator.Send(new CheckRoleQuery(roleName));
        if (result)
            return Ok(ApiResponse<bool>.Success(result));
        else
            return Ok(ApiResponse<bool>.Fail("Нет доступа"));
        
    }
    
    /// <summary>
    /// Обновить разрешения для роли
    /// </summary>
    [HttpPut(nameof(UpdateRolePermission))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<int>))]
    public async Task<IActionResult> UpdateRolePermission([FromBody] UpdateRolePermissionRequest request)
    {
        var result = await Mediator.Send(new UpdateRolePermissionCommand(request));
        return Ok(ApiResponse<int>.Success(result));
    }
}