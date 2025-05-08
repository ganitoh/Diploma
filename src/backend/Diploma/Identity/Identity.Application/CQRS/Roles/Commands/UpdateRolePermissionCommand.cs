using Common.Application;
using Common.Application.Exceptions;
using Common.Infrastructure.UnitOfWork;
using Identity.ApplicatinContract.Requests;
using Identity.Application.Common.Persistance.Repositories;

namespace Identity.Application.CQRS.Roles.Commands;

/// <summary>
/// Обновить разрешения для роли
/// </summary>
public record UpdateRolePermissionCommand(UpdateRolePermissionRequest RequestData) : ICommand<int>;

internal class UpdateRolePermissionCommandHandler : ICommandHandler<UpdateRolePermissionCommand, int>
{
    private readonly IUnitOfWork  _unitOfWork;
    private readonly IRolePermissionRepository _rolePermissionRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPermissionRepository _permissionRepository;

    public UpdateRolePermissionCommandHandler(
        IUnitOfWork unitOfWork, 
        IRolePermissionRepository rolePermissionRepository, 
        IRoleRepository roleRepository,
        IPermissionRepository permissionRepository)
    {
        _unitOfWork = unitOfWork;
        _rolePermissionRepository = rolePermissionRepository;
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
    }

    public async Task<int> Handle(UpdateRolePermissionCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetById(request.RequestData.RoleId, cancellationToken)
                   ?? throw new NotFoundException("Role not found");

        var permissions =
            await _permissionRepository.GetPermissionsByIdsAsync(request.RequestData.PermissionIds, cancellationToken);

        if (!permissions.Any())
            throw new NotFoundException("Permissions not found");

        role.Permissions = permissions;
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return role.Id;
    }
}