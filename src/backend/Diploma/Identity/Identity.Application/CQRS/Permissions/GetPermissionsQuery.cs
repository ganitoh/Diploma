using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Application;
using Identity.ApplicatinContract.Dtos;
using Identity.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.CQRS.Permissions;

/// <summary>
/// Получить все разрешения
/// </summary>
public record GetPermissionsQuery : IQuery<ICollection<PermissionDto>>;

class GetPermissionsQueryHandler : IQueryHandler<GetPermissionsQuery, ICollection<PermissionDto>>
{
    private readonly IdentityDbContext _context;
    private readonly IMapper _mapper;

    public GetPermissionsQueryHandler(IdentityDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<ICollection<PermissionDto>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Permissions
            .AsNoTracking()
            .ProjectTo<PermissionDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}