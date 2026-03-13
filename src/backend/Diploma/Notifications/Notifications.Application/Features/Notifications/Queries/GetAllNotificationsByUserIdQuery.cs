using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Application;
using Microsoft.EntityFrameworkCore;
using Notifications.Application.Common.Persistance;
using Notifications.ApplicationContract.Dtos;

namespace Notifications.Application.Features.Notifications.Queries;

public record GetAllNotificationsByUserIdQuery(Guid UserId) : IQuery<ICollection<NotificationDto>>;

/// <inheritdoc />
internal class GetAllNotificationsByUserIdQueryHandler : IQueryHandler<GetAllNotificationsByUserIdQuery,  ICollection<NotificationDto>>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyNotificationDbContext _context;

    public GetAllNotificationsByUserIdQueryHandler(IMapper mapper, IReadOnlyNotificationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }


    public async Task<ICollection<NotificationDto>> Handle(GetAllNotificationsByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Notifications
            .Where(x => x.UserId == request.UserId)
            .ProjectTo<NotificationDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}

