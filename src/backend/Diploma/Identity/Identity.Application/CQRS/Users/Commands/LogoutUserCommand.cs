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
public record LogoutUserCommand(Guid UserId) : ICommand<Unit>;

/// <inheritdoc/>
internal class LogoutUserCommandHandler : ICommandHandler<LogoutUserCommand, Unit>
{
    private readonly IdentityDbContext _context;

    public LogoutUserCommandHandler(IdentityDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.UserId == request.UserId && !x.IsRevoked,cancellationToken);

        if (refreshToken is not null)
        {
            refreshToken.IsRevoked = true;
            await _context.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}