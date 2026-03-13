using AutoMapper;
using Common.Application;
using Common.Application.Persistance;
using Organizaiton.Application.Common.Persistance;
using Organization.ApplicationContract.Requests;
using Organization.Domain.ValueObjects;

namespace Organization.Application.Features.Organizations.Commands;

/// <summary>
/// Команда для создания организации
/// </summary>
/// <param name="OrganizationData"></param>
public record CreateOrganizationCommand(CreateOrganizationRequest OrganizationData, Guid UserId) : ICommand<int>;

/// <summary>
/// Хендлер команды для создания организации
/// </summary>
internal class CreateOrganizationCommandHandler : ICommandHandler<CreateOrganizationCommand, int>
{
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper  _mapper;

    public CreateOrganizationCommandHandler(
        IOrganizationRepository organizationRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _organizationRepository = organizationRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
    {
        if (!request.OrganizationData.IsExternal)
        {
            var organizationByUserId = await _organizationRepository.GetOrganizationByUserIdAsync(request.UserId, cancellationToken);

            if (organizationByUserId is not null)
                throw new ApplicationException("Организаця уже существует");
        }

        var address = _mapper.Map<Address>(request.OrganizationData.LegalAddress);
        
        var organization = new Organization.Domain.Models.Organization(
            request.OrganizationData.Name,
            request.OrganizationData.Inn,
            address,
            new Email(request.OrganizationData.Email));
        
        organization.AddUser(request.UserId);
        
        _organizationRepository.Create(organization);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return organization.Id;
    }
}