using Chat.ApplicationContract.Dtos;
using Chat.Domain.Models;

namespace Chat.Application.Profile;

public class ChatProfile  : AutoMapper.Profile
{
    public ChatProfile()
    {
        CreateMap<Domain.Models.Chat, ChatDto>();
        CreateMap<Message, MessageDto>();
    }
}