using Common.Application;
using Common.Application.Exceptions;
using Identity.Application.Common.Auth;
using Identity.Application.Common.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.CQRS.Users.Queries;

public record class  LoginUserQuery(string Password, string Email) : IQuery<string>;

internal class LoginUserQueryHandler : IQueryHandler<LoginUserQuery, string>
{
    private readonly IIdentityDbContext _identityDbContext;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    public LoginUserQueryHandler(
        IIdentityDbContext identityDbContext,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider)
    {
        _identityDbContext = identityDbContext;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }

    public async Task<string> Handle(LoginUserQuery query, CancellationToken cancellationToken)
    {
        var user = await _identityDbContext.Users
            .FirstOrDefaultAsync(u => u.Email == query.Email,cancellationToken);

        if (user is null)
            throw new NotFoundException("Пользователь не найден");

        if (!_passwordHasher.Verify(query.Password, user.PasswordHash))
            throw new ApplicationException("Неверный пароль");

        var token = _jwtProvider.GenerateToken(user);
        
        return token;
    }
}