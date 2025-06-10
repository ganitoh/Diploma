using Common.API;
using Identity.ApplicatinContract.Requests;
using Identity.Application.CQRS.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers;

public class IdentityController : BaseApiController
{

    /// <summary>
    /// Обновление access токена
    /// </summary>
    /// <returns></returns>
    [HttpPost(nameof(Refresh))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
    public async Task<IActionResult> Refresh()
    {
        var response = await Mediator.Send(new AccessTokenRefreshCommand());
        return  Ok(ApiResponse<string>.Success(response));
    }
    
    /// <summary>
    /// Вход пользователя в систему
    /// </summary>
    [HttpPost(nameof(Login))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
    {
        var response = await Mediator.Send(new LoginUserCommand(request));
        return Ok(ApiResponse<string>.Success(response));
    }
    
    /// <summary>
    /// Выход пользователя из системы
    /// </summary>
    [Authorize]
    [HttpPost(nameof(Logout))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<Unit>))]
    public async Task<IActionResult> Logout()
    {
        var response = await Mediator.Send(new LogoutUserCommand());
        return Ok(ApiResponse<Unit>.Success(response));
    }

    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    [HttpPost(nameof(Registration))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
    public async Task<IActionResult> Registration([FromBody] CreateUserRequest request)
    {
        var response = await Mediator.Send(new CreateUserCommand(request));
        return Ok(ApiResponse<Guid>.Success(response));
    }
}