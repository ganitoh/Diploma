using AutoMapper;
using Common.Application;
using Common.Application.Exceptions;
using Common.Application.Persistance;
using Organizaiton.Application.Common.Persistance;
using Organization.ApplicationContract.Requests;
using Organization.Domain.ValueObjects;

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
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateOrganizationDataCommandHandler(IOrganizationRepository organizationRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _organizationRepository = organizationRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(UpdateOrganizationDataCommand request, CancellationToken cancellationToken)
    {
        var organization = await _organizationRepository.GetByIdAsync(request.RequestData.OrganizationId); 
        
        if (organization is null)
            throw new NotFoundException("Организация не найдена");
        
        var address = _mapper.Map<Address>(request.RequestData.LegalAddress);
        
        organization.ChangeLegalAddress(address);
        organization.ChangeName(request.RequestData.Name);
        organization.ChangeEmail(new Email(request.RequestData.Email));
        organization.ChangeDescription(request.RequestData.Description);
        organization.ChangeInn(request.RequestData.Inn);
        organization.UnApprove();
        
        await _unitOfWork.CommitAsync(cancellationToken);
        return organization.Id;
    }
}