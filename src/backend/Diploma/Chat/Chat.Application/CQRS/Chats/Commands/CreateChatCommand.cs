using System.Security.Claims;
using Chat.ApplicationContract.Requests;
using Chat.Infrastructure.Persistance.Context;
using Common.Application;
using Microsoft.AspNetCore.Http;

namespace Chat.Application.CQRS.Chats.Commands;

/// <summary>
/// Команда для создания чата
/// </summary>
public record CreateChatCommand(CreateChatRequest Data) : ICommand<int>;

///<inheritdoc />
internal class CreateChatCommandHandler : ICommandHandler<CreateChatCommand, int>
{
    private readonly ChatDbContext _context;
    private readonly IHttpContextAccessor _contextAccessor;

    public CreateChatCommandHandler(ChatDbContext context, IHttpContextAccessor contextAccessor)
    {
        _context = context;
        _contextAccessor = contextAccessor;
    }

    public async Task<int> Handle(CreateChatCommand request, CancellationToken cancellationToken)
    {
        var firstUserId = _contextAccessor.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier)
            .Value;
        var chat = new Domain.Models.Chat
        {
            FirstUserId = Guid.Parse(firstUserId),
            SecondUserId = Guid.Parse(request.Data.SecondUserId),
            OrderId = request.Data.OrderIsd
        };
        
        _context.Chats.Add(chat);
        await _context.SaveChangesAsync(cancellationToken);

        return chat.Id;
    }
}