using Common.API;
using Identity.ApplicatinContract.Dtos;
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
        var refreshToken = HttpContext.Request.Cookies["refreshToken"];
        if (string.IsNullOrEmpty(refreshToken))
            throw new UnauthorizedAccessException("Токен не найден");
        
        var response = await Mediator.Send(new AccessTokenRefreshCommand(refreshToken));
        SetNewTokens(response);
        
        return  Ok(ApiResponse<string>.Success(response.AccessToken));
    }
    
    /// <summary>
    /// Вход пользователя в систему
    /// </summary>
    [HttpPost(nameof(Login))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<Unit>))]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
    {
        var response = await Mediator.Send(new LoginUserCommand(request));
        SetNewTokens(response);
        
        return Ok(ApiResponse<Unit>.Success(Unit.Value));
    }
    
    /// <summary>
    /// Выход пользователя из системы
    /// </summary>
    [Authorize]
    [HttpPost(nameof(Logout))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<Unit>))]
    public async Task<IActionResult> Logout()
    {
        var response = await Mediator.Send(new LogoutUserCommand(GetUserId()));
        DeleteTokens();
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
    
    #region Token halpers
    
    [NonAction]
    private void SetNewTokens(TokenDto tokens)
    {
        HttpContext.Response.Cookies.Append("access_token", tokens.AccessToken, new CookieOptions
        {
            HttpOnly = false,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTimeOffset.UtcNow.AddDays(1)
        });
        
        HttpContext.Response.Cookies.Append("refreshToken", tokens.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(7)
        });
    }
    
    [NonAction]
    private void DeleteTokens()
    {
        HttpContext.Response.Cookies.Delete("access_token", new CookieOptions
        {
            HttpOnly = false,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTimeOffset.UtcNow.AddDays(1)
        });
        
        HttpContext.Response.Cookies.Delete("refreshToken", new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(7)
        });
    }
    
    #endregion
}