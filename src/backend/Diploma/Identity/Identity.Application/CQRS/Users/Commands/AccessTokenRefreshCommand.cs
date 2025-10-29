using Common.Application;
using Identity.ApplicatinContract.Dtos;
using Identity.Domain.Models;
using Identity.Infrastructure.Auth.Abstractions;
using Identity.Infrastructure.Persistance.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.CQRS.Users.Commands;

/// <summary>
/// Команда на обновление access токена
/// </summary>
public record AccessTokenRefreshCommand(string RefreshToken) : ICommand<TokenDto>;

internal class AccessTokenRefreshCommandHandler : ICommandHandler<AccessTokenRefreshCommand, TokenDto>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IdentityDbContext _context;

    public AccessTokenRefreshCommandHandler(
        IJwtProvider jwtProvider,
        IdentityDbContext context)
    {
        _jwtProvider = jwtProvider;
        _context = context;
    }

    public async Task<TokenDto> Handle(AccessTokenRefreshCommand request, CancellationToken cancellationToken)
    {
        var storedToken = await _context.RefreshTokens
            .Include(rt => rt.User)
            .ThenInclude(u => u.Role)
            .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken && !rt.IsRevoked, cancellationToken);

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
            UserId = user.Id,
            IsRevoked = false
        });

        await _context.SaveChangesAsync(cancellationToken);

        return new TokenDto(newAccessToken, newRefreshToken);
    }
}