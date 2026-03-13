using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Application;
using Microsoft.EntityFrameworkCore;
using Notifications.ApplicationContract.Dtos;
using Notifications.Infrastructure.Persistance.Context;

namespace Notifications.Application.CQRS.Notifications.Queries;

/// <summary>
/// Получить все непрочитанные уведомления пользователя
/// </summary>
/// <param name="UserId"></param>
public record GetNotReadNotificationByUserQuery(Guid UserId) : IQuery<ICollection<NotificationDto>>;


/// <inheritdoc />
internal class GetNotReadNotificationByUserQueryHandler : IQueryHandler<GetNotReadNotificationByUserQuery,  ICollection<NotificationDto>>
{
    private readonly NotificationDbContext _context;
    private readonly IMapper _mapper;

    public GetNotReadNotificationByUserQueryHandler(NotificationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<NotificationDto>> Handle(GetNotReadNotificationByUserQuery request, CancellationToken cancellationToken)
    {
        return await _context.Notifications
            .AsNoTracking()
            .Where(x => x.UserId == request.UserId && !x.IsRead)
            .ProjectTo<NotificationDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}