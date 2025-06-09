using Common.Application;
using Identity.Domain.Models;
using Identity.Infrastructure.Auth.Abstractions;
using Identity.Infrastructure.Persistance.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.CQRS.Users.Commands;

/// <summary>
/// Команда на обновление access токена
/// </summary>
public record AccessTokenRefreshCommand : ICommand<string>;

internal class AccessTokenRefreshCommandHandler : ICommandHandler<AccessTokenRefreshCommand, string>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IdentityDbContext _context;

    public AccessTokenRefreshCommandHandler(
        IJwtProvider jwtProvider,
        IHttpContextAccessor httpContextAccessor,
        IdentityDbContext context)
    {
        _jwtProvider = jwtProvider;
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }

    public async Task<string> Handle(AccessTokenRefreshCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = _httpContextAccessor.HttpContext.Request.Cookies["refreshToken"];
        if (string.IsNullOrEmpty(refreshToken))
            throw new UnauthorizedAccessException("Токен не найден");

        var storedToken = await _context.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == refreshToken && !rt.IsRevoked, cancellationToken);

        if (storedToken == null || storedToken.Expires < DateTime.UtcNow)
            throw new UnauthorizedAccessException("Токен не найден");

        var user = storedToken.User;
        var newAccessToken = _jwtProvider.GenerateToken(user);
        var newRefreshToken = _jwtProvider.GenerateRefreshToken();

        storedToken.IsRevoked = true;

        _context.RefreshTokens.Add(new RefreshToken
        {
            Token = newRefreshToken,
            Expires = DateTime.UtcNow.AddDays(7),
            UserId = user.Id
        });

        await _context.SaveChangesAsync(cancellationToken);

        _httpContextAccessor.HttpContext.Response.Cookies.Append("access_token", newAccessToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTimeOffset.UtcNow.AddDays(1)
        });
        
        _httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(7)
        });

        return newAccessToken;
    }
}