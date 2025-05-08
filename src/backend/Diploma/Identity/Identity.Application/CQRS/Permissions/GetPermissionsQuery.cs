using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Application;
using Identity.ApplicatinContract.Dtos;
using Identity.Application.Common.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.CQRS.Permissions;

/// <summary>
/// Получить все разрешения
/// </summary>
public record GetPermissionsQuery : IQuery<ICollection<PermissionDto>>;

class GetPermissionsQueryHandler : IQueryHandler<GetPermissionsQuery, ICollection<PermissionDto>>
{
    private readonly IIdentityDbContext _context;
    private readonly IMapper _mapper;

    public GetPermissionsQueryHandler(IIdentityDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<ICollection<PermissionDto>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Permissions
            .ProjectTo<PermissionDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}