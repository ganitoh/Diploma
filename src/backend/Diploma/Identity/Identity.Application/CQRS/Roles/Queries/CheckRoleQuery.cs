using System.Security.Claims;
using Common.Application;
using Microsoft.AspNetCore.Http;

namespace Identity.Application.CQRS.Roles.Queries;

/// <summary>
/// Првоеряет роль на совпадение с пользователем
/// </summary>
public record CheckRoleQuery(string RoleName) : IQuery<bool>;

internal class CheckRoleQueryHandler : IQueryHandler<CheckRoleQuery, bool>
{
    private readonly IHttpContextAccessor  _httpContextAccessor;

    public CheckRoleQueryHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<bool> Handle(CheckRoleQuery request, CancellationToken cancellationToken)
    {
        var userRole =
            _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ??
            throw new ApplicationException("Роль пользователя не найдена");

        return Task.FromResult(userRole == request.RoleName);
    }
}