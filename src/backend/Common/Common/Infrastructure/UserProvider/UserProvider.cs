using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Common.Infrastructure.UserProvider;

public class UserProvider : IUserProvider
{
    private readonly IHttpContextAccessor _accessor;

    public UserProvider(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public Guid GetUserId()
    {
        return Guid.TryParse(_accessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var guid)
            ? guid
            : Guid.Empty;
    }

    public string? GetUserName()
    {
        return _accessor.HttpContext?.User.FindFirst("name")?.Value;
    }
}