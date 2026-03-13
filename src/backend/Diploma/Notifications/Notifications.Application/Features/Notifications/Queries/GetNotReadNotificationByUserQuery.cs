using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Application;
using Microsoft.EntityFrameworkCore;
using Notifications.Application.Common.Persistance;
using Notifications.ApplicationContract.Dtos;

namespace Notifications.Application.CQRS.Notifications.Queries;

public record GetNotReadNotificationByUserQuery(Guid UserId) : IQuery<ICollection<NotificationDto>>;


/// <inheritdoc />
internal class GetNotReadNotificationByUserQueryHandler : IQueryHandler<GetNotReadNotificationByUserQuery,  ICollection<NotificationDto>>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyNotificationDbContext _context;

    public GetNotReadNotificationByUserQueryHandler(IMapper mapper, IReadOnlyNotificationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ICollection<NotificationDto>> Handle(GetNotReadNotificationByUserQuery request, CancellationToken cancellationToken)
    {
        return await _context.Notifications
            .Where(x => x.UserId == request.UserId && !x.IsRead)
            .ProjectTo<NotificationDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}