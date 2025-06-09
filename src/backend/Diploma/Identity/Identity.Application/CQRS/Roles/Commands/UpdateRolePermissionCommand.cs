using Common.Application;
using Common.Application.Exceptions;
using Identity.ApplicatinContract.Requests;
using Identity.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.CQRS.Roles.Commands;

/// <summary>
/// Обновить разрешения для роли
/// </summary>
public record UpdateRolePermissionCommand(UpdateRolePermissionRequest RequestData) : ICommand<int>;

internal class UpdateRolePermissionCommandHandler : ICommandHandler<UpdateRolePermissionCommand, int>
{
    private readonly IdentityDbContext _context;

    public UpdateRolePermissionCommandHandler(IdentityDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(UpdateRolePermissionCommand request, CancellationToken cancellationToken)
    {
        var role = await _context.Roles.FirstOrDefaultAsync(x => x.Id == request.RequestData.RoleId, cancellationToken)
                   ?? throw new NotFoundException("Role not found");

        var permissions = await _context.Permissions
            .Where(x=>request.RequestData.PermissionIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        if (!permissions.Any())
            throw new NotFoundException("Permissions not found");

        role.Permissions = permissions;
        await _context.SaveChangesAsync(cancellationToken);
        
        return role.Id;
    }
}