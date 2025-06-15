using Chat.ApplicationContract.Dtos;

namespace Chat.Application.SignalR;

public interface IChatClient
{
    public Task ReceiveMessagesAsync(MessageDto message);
}