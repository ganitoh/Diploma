using Common.API;
using Identity.ApplicatinContract.Requests;
using Identity.Application.CQRS.Users.Commands;
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
        var result = await Mediator.Send(new LoginUserCommand(request));
        return Ok(ApiResponse<string>.Success(result));
    }
    
    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    [HttpPost(nameof(Registration))]
    [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(ApiResponse<string>))]
    public async Task<IActionResult> Registration([FromBody]CreateUserRequest request) =>
        Ok(ApiResponse<Guid>.Success(await Mediator.Send(new CreateUserCommand(request))));
}