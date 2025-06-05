using AutoMapper;
using Common.Application;
using Common.Application.Exceptions;
using Identity.ApplicatinContract.Dtos;
using Identity.ApplicatinContract.Requests;
using Identity.Application.Common.Auth;
using Identity.Application.Common.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.CQRS.Users.Queries;

public record class  LoginUserQuery(LoginUserRequest queryData) : IQuery<string>;

internal class LoginUserQueryHandler : IQueryHandler<LoginUserQuery, string>
{
    private readonly IIdentityDbContext _identityDbContext;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;
    private readonly IHttpContextAccessor _httpContext;

    public LoginUserQueryHandler(
        IIdentityDbContext identityDbContext,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider,
        IHttpContextAccessor httpContext)
    {
        _identityDbContext = identityDbContext;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
        _httpContext = httpContext;
    }

    public async Task<string> Handle(LoginUserQuery query, CancellationToken cancellationToken)
    {
        var user = await _identityDbContext.Users
            .Include(x => x.Role)
            .FirstOrDefaultAsync(u => u.Email == query.queryData.Email, cancellationToken);

        if (user is null)
            throw new NotFoundException("Пользователь не найден");

        if (!_passwordHasher.Verify(query.queryData.Password, user.PasswordHash))
            throw new ApplicationException("Неверный пароль");

        var token = _jwtProvider.GenerateToken(user);
        
        _httpContext.HttpContext.Response.Cookies.Append("access_token", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTimeOffset.UtcNow.AddDays(1)
        });
        
        return user.Id.ToString();
    }
}