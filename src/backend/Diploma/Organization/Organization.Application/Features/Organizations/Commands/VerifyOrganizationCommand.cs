using Common.Application;
using Common.Application.Exceptions;
using Common.Application.Persistance;
using MediatR;
using Organizaiton.Application.Common.Persistance;

namespace Organization.Application.Features.Organizations.Commands;

/// <summary>
/// Команда для верификации организации админом
/// </summary>
public record VerifyOrganizationCommand(int OrganizationId) : ICommand<Unit>;

/// <inheritdoc />
internal class VerifyOrganizationCommandHandler : ICommandHandler<VerifyOrganizationCommand, Unit>
{
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public VerifyOrganizationCommandHandler(IOrganizationRepository organizationRepository, IUnitOfWork unitOfWork)
    {
        _organizationRepository = organizationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(VerifyOrganizationCommand request, CancellationToken cancellationToken)
    {
        var organization = await _organizationRepository.GetByIdAsync(request.OrganizationId);
        
        if(organization is null)
            throw new NotFoundException("Организация не найдена");

        organization.Approve();
        
        await _unitOfWork.CommitAsync(cancellationToken);
        return Unit.Value;
    }
}