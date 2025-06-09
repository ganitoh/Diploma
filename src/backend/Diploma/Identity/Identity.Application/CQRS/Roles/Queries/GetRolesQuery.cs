using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Application;
using Identity.ApplicatinContract.Dtos;
using Identity.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.CQRS.Roles.Queries;

/// <summary>
/// Получить все роли
/// </summary>
public record GetRolesQuery : IQuery<ICollection<RoleDto>>;

internal class GetRolesQueryHandler : IQueryHandler<GetRolesQuery, ICollection<RoleDto>>
{
    private readonly IdentityDbContext  _context;
    private readonly IMapper _mapper;

    public GetRolesQueryHandler(IdentityDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Roles
            .AsNoTracking()
            .ProjectTo<RoleDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}