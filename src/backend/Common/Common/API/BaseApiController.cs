using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Common.API;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class BaseApiController : ControllerBase
{
    private ISender _mediator = null!;
    
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    
    /// <summary>
    /// Получить идентификатор пользователя
    /// </summary>
    protected Guid GetUserId()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ??
                     throw new ApplicationException("Идентификатор пользователя не нйден");

        return new Guid(userId);
    }
}