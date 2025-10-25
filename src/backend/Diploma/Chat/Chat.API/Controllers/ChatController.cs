using Chat.Application.CQRS.Chats.Commands;
using Chat.Application.CQRS.Chats.Queries;
using Chat.ApplicationContract.Dtos;
using Chat.ApplicationContract.Requests;
using Common.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.API.Controllers;

public class ChatController : BaseApiController
{
    /// <summary>
    /// Поулчить чат
    /// </summary>
    [Authorize]
    [HttpGet(nameof(GetChat))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<ChatDto>))]
    public async Task<IActionResult> GetChat([FromQuery]int orderId)
    {
        var response = await Mediator.Send(new GetChatQuery(orderId));
        return Ok(ApiResponse<ChatDto>.Success(response));
    }
    
    /// <summary>
    /// Создать чат
    /// </summary>
    [Authorize]
    [HttpPost(nameof(CreateChat))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<int>))]
    public async Task<IActionResult> CreateChat([FromBody]CreateChatRequest request)
    {
        var response = await Mediator.Send(new CreateChatCommand(request, User));
        return Ok(ApiResponse<int>.Success(response));
    }
}