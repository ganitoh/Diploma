using Common.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notifications.Application.CQRS.Notifications.Commands;
using Notifications.ApplicationContract.Requests;

namespace Notifications.API.Controllers;

public class NotificationController : BaseApiController
{
    /// <summary>
    /// Создать уведомление
    /// </summary>
    [Authorize]
    [HttpPost(nameof(GetSellOrderAnalytics))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<int>))]
    public async Task<IActionResult> GetSellOrderAnalytics([FromBody] CreateNotificationRequest requestData)
    {
        var result = await Mediator.Send(new CreateNotificationCommand(requestData));
        return Ok(ApiResponse<int>.Success(result));
    }
}