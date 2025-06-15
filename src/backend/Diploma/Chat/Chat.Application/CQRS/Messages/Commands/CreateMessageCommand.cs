using Chat.ApplicationContract.Requests;
using Chat.Domain.Models;
using Chat.Infrastructure.Persistance.Context;
using Common.Application;

namespace Chat.Application.CQRS.Messages.Commands;

/// <summary>
/// Команда для создания сообщения
/// </summary>
public record CreateMessageCommand(CreateMessageRequest Data) : ICommand<int>;

/// <inheritdoc />
internal class CreateMessageCommandHandler : ICommandHandler<CreateMessageCommand, int>
{
    private readonly ChatDbContext _context;

    public CreateMessageCommandHandler(ChatDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var message = new Message
        {
            Text = request.Data.Text,
            CreatedDatetime = DateTime.UtcNow,
            UserId = request.Data.UserId,
            ChatId = request.Data.ChatId
        };
        
        _context.Messages.Add(message);
        await _context.SaveChangesAsync(cancellationToken);
        return message.Id;
    }
}