using Common.API;
using Identity.Application.CQRS.Users.Commands;
using Identity.Application.CQRS.Users.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers;

public class IdentityController : BaseApiController
{
    
    /// <summary>
    /// Вход пользователя в систему
    /// </summary>
    [HttpGet(nameof(Login))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
    public async Task<IActionResult> Login([FromQuery] LoginUserQuery command)
    {
        var jwtToken = await Mediator.Send(command);
        Response.Cookies.Append("access_token", jwtToken);
        return Ok(ApiResponse<string>.Success(jwtToken));
    }
    
    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    [HttpPost(nameof(Registration))]
    [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(ApiResponse<string>))]
    public async Task<IActionResult> Registration([FromBody]CreateUserCommand command) =>
        Ok(ApiResponse<Guid>.Success(await Mediator.Send(command)));
}