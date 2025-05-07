using Common.Application;
using Common.Application.Exceptions;
using Common.Infrastructure.UnitOfWork;
using MediatR;
using Organization.Application.Commnon.Persistance.Repositories;
using Organization.ApplicationContract.Requests;

namespace Organization.Application.CQRS.Organizations.Commands;

/// <summary>
/// Команда на обновление данных организации
/// </summary>
public record class UpdateOrganizationDataCommand(UpdateOrganizationDataRequest RequestData) : ICommand<int>;

/// <summary>
/// Хандлер для команды на обновление данных организации
/// </summary>
internal class UpdateOrganizationDataCommandHandler : ICommandHandler<UpdateOrganizationDataCommand, int>
{
    private readonly IOrganizationRepository _repository;
    private readonly IUnitOfWork  _unitOfWork;

    public UpdateOrganizationDataCommandHandler(IOrganizationRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(UpdateOrganizationDataCommand request, CancellationToken cancellationToken)
    {
        var organization = await _repository.GetById(request.RequestData.OrganizationId, cancellationToken) ??
                           throw new NotFoundException("Организация не найдена");
        
        organization.Name = request.RequestData.Name;
        organization.Email = request.RequestData.Email;
        organization.Description = request.RequestData.Description;
        organization.Inn = request.RequestData.Inn;
        organization.LegalAddress = request.RequestData.LegalAddress;
        organization.IsApproval = false;
        
        await _unitOfWork.CommitAsync(cancellationToken);
        return organization.Id;
    }
}