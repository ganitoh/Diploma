using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Application;
using Microsoft.EntityFrameworkCore;
using Notifications.ApplicationContract.Dtos;
using Notifications.Infrastructure.Persistance.Context;

namespace Notifications.Application.CQRS.Notifications.Queries;

/// <summary>
/// Запрос на получения всех уведомлений пользователя
/// </summary>
/// <param name="UserId"></param>
public record GetAllNotificationsByUserIdQuery(Guid UserId) : IQuery<ICollection<NotificationDto>>;

/// <inheritdoc />
internal class GetAllNotificationsByUserIdQueryHandler : IQueryHandler<GetAllNotificationsByUserIdQuery,  ICollection<NotificationDto>>
{
    private readonly NotificationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllNotificationsByUserIdQueryHandler(NotificationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<NotificationDto>> Handle(GetAllNotificationsByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Notifications
            .AsNoTracking()
            .Where(x => x.UserId == request.UserId)
            .ProjectTo<NotificationDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}

