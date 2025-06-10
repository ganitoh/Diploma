using System.Security.Claims;
using Common.Application;
using Common.Application.Exceptions;
using Identity.Infrastructure.Persistance.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.CQRS.Users.Commands;

/// <summary>
/// Команда для выхода пользоавтеля из систем
/// </summary>
public class LogoutUserCommand : ICommand<Unit>;

/// <inheritdoc/>
internal class LogoutUserCommandHanler : ICommandHandler<LogoutUserCommand, Unit>
{
    private readonly IdentityDbContext _context;
    private readonly IHttpContextAccessor  _httpContextAccessor;

    public LogoutUserCommandHanler(IdentityDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Unit> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        var userId =
            _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)
                ?.Value ?? throw new NotFoundException("Пользователь не найден");
        
        var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.UserId == Guid.Parse(userId) && !x.IsRevoked,cancellationToken);

        if (refreshToken is not null)
        {
            refreshToken.IsRevoked = true;
            await _context.SaveChangesAsync(cancellationToken);
        }
        
        _httpContextAccessor.HttpContext.Response.Cookies.Delete("access_token", new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTimeOffset.UtcNow.AddDays(1)
        });
        
        _httpContextAccessor.HttpContext.Response.Cookies.Delete("refreshToken", new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddDays(7)
        });

        return Unit.Value;
    }
}