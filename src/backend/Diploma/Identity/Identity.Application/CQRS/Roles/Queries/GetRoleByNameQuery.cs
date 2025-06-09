using AutoMapper;
using Common.Application;
using Common.Application.Exceptions;
using Identity.ApplicatinContract.Dtos;
using Identity.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.CQRS.Roles.Queries;

/// <summary>
/// Получить роль по названию
/// </summary>
public record GetRoleByNameQuery(string RoleName) : IQuery<RoleDto>;

internal class GetRoleByNameQueryHandler : IQueryHandler<GetRoleByNameQuery, RoleDto>
{
    private readonly IdentityDbContext _context;
    private readonly IMapper _mapper;

    public GetRoleByNameQueryHandler(IdentityDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<RoleDto> Handle(GetRoleByNameQuery request, CancellationToken cancellationToken)
    {
        var role = await  _context.Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(x=>x.Name == request.RoleName, cancellationToken);

        if (role is null)
            throw new NotFoundException("Role not found");

        return _mapper.Map<RoleDto>(role);
    }
}