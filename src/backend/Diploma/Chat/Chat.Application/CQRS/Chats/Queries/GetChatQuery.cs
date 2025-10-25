using System.Security.Claims;
using AutoMapper;
using Chat.ApplicationContract.Dtos;
using Chat.Infrastructure.Persistance.Context;
using Common.Application;
using Common.Application.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Chat.Application.CQRS.Chats.Queries;

/// <summary>
/// Поулчить чат
/// </summary>
public record GetChatQuery(int OrderId) : IQuery<ChatDto>;

/// <inheritdoc/>
internal class GetChatQueryHandler :  IQueryHandler<GetChatQuery, ChatDto>
{
    private readonly IMapper  _mapper;
    private readonly ChatDbContext _context;

    public GetChatQueryHandler(IMapper mapper, ChatDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ChatDto> Handle(GetChatQuery request, CancellationToken cancellationToken)
    {
        var chat = await  _context.Chats
            .AsNoTracking()
            .Include(x => x.Messages)
            .FirstOrDefaultAsync(x => x.OrderId == request.OrderId, cancellationToken) 
                   ?? throw new NotFoundException("Чат не найден"); 
        
        return _mapper.Map<ChatDto>(chat);
    }
}