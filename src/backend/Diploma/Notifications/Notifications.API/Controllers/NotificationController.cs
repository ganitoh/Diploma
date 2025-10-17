using System.Security.Claims;
using Common.API;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notifications.Application.CQRS.Notifications.Commands;
using Notifications.Application.CQRS.Notifications.Queries;
using Notifications.ApplicationContract.Dtos;
using Notifications.ApplicationContract.Requests;

namespace Notifications.API.Controllers;

public class NotificationController : BaseApiController
{

    /// <summary>
    /// Получить все уведомления пользователя
    /// </summary>
    [Authorize]
    [HttpGet(nameof(GetNotificationsByUser))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<ICollection<NotificationDto>>))]
    public async Task<IActionResult> GetNotificationsByUser()
    {
        var userId =
            HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ??
            throw new ApplicationException("Идентификатор пользователя не нйден");

        var result = await Mediator.Send(new GetAllNotificationsByUserIdQuery(Guid.Parse(userId)));
        return Ok(ApiResponse<ICollection<NotificationDto>>.Success(result));
    }
    
    /// <summary>
    /// Получить все не прочитанные уведомления пользователя
    /// </summary>
    [Authorize]
    [HttpGet(nameof(GetNotReadNotificationsByUser))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<ICollection<NotificationDto>>))]
    public async Task<IActionResult> GetNotReadNotificationsByUser()
    {
        var userId =
            HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ??
            throw new ApplicationException("Идентификатор пользователя не нйден");

        var result = await Mediator.Send(new GetNotReadNotificationByUserQuery(Guid.Parse(userId)));
        return Ok(ApiResponse<ICollection<NotificationDto>>.Success(result));
    }
    
    /// <summary>
    /// Создать уведомление
    /// </summary>
    [Authorize]
    [HttpPost(nameof(CreateNotification))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<int>))]
    public async Task<IActionResult> CreateNotification([FromBody] CreateNotificationRequest requestData)
    {
        var result = await Mediator.Send(new CreateNotificationCommand(requestData));
        return Ok(ApiResponse<int>.Success(result));
    }
    
    /// <summary>
    /// Отметить уведомление как прочитанное
    /// </summary>
    [Authorize]
    [HttpPost(nameof(ReadNotification))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<Unit>))]
    public async Task<IActionResult> ReadNotification([FromQuery] int notificationId)
    {
        await Mediator.Send(new ReadNotificationCommand(notificationId));
        return Ok(ApiResponse<Unit>.Success(Unit.Value));
    }
}