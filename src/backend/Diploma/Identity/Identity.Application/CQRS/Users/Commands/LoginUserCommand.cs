using Common.Application;
using Common.Application.Exceptions;
using Identity.ApplicatinContract.Dtos;
using Identity.ApplicatinContract.Requests;
using Identity.Domain.Models;
using Identity.Infrastructure.Auth.Abstractions;
using Identity.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.CQRS.Users.Commands;

/// <summary>
/// Запрос на авторизацию пользователя
/// </summary>
public record class LoginUserCommand(LoginUserRequest Data) : IQuery<TokenDto>;

/// <inheritdoc/>
internal class LoginUserCommandHandler : IQueryHandler<LoginUserCommand, TokenDto>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IdentityDbContext _context;

    public LoginUserCommandHandler(
        IdentityDbContext identityDbContext,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider)
    {
        _context = identityDbContext;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }

    public async Task<TokenDto> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .AsNoTracking()
            .Include(x => x.Role)
            .FirstOrDefaultAsync(u => u.Email == command.Data.Email, cancellationToken);

        if (user is null)
            throw new NotFoundException("Пользователь не найден");

        if (!_passwordHasher.Verify(command.Data.Password, user.PasswordHash))
            throw new ApplicationException("Неверный пароль");

        var token = _jwtProvider.GenerateToken(user);
        var refreshToken = await GenerateRefreshTokenAsync(user, cancellationToken);

        return new TokenDto(token, refreshToken);
    }

    private async Task<string> GenerateRefreshTokenAsync(User user, CancellationToken cancellationToken)
    {
        var refreshTokenString = _jwtProvider.GenerateRefreshToken();

        var refreshToken = new RefreshToken()
        {
            Token = refreshTokenString,
            Expires = DateTime.UtcNow.AddDays(7),
            UserId = user.Id,
        };
        
        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return  refreshToken.Token;
    }
}