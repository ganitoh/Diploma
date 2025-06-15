using System.Security.Claims;
using AutoMapper;
using Chat.ApplicationContract.Dtos;
using Chat.Infrastructure.Persistance.Context;
using Common.Application;
using Common.Application.Exceptions;
using Microsoft.AspNetCore.Http;
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
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetChatQueryHandler(IMapper mapper, ChatDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper;
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ChatDto> Handle(GetChatQuery request, CancellationToken cancellationToken)
    {
        var firstUserId = _httpContextAccessor.HttpContext.User.Claims.First(x=>x.Type == ClaimTypes.NameIdentifier).Value;
        
        var chat = await  _context.Chats
            .AsNoTracking()
            .FirstOrDefaultAsync(x=>x.FirstUserId == Guid.Parse(firstUserId) && x.OrderId == request.OrderId, cancellationToken) 
                   ?? throw new NotFoundException("Чат не найден"); 
        
        return _mapper.Map<ChatDto>(chat);
    }
}