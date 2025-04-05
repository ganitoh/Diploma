using AutoMapper;
using Common.Application;
using Common.Infrastructure.UnitOfWork;
using Organization.ApplicationContract.Requests;
using Organization.Application.Commnon.Persistance.Repositories;

namespace Organization.Application.CQRS.Organizations.Commands;

/// <summary>
/// Команда для создания организации
/// </summary>
/// <param name="Data"></param>
public record CreateOrganizationCommand(CreateOrganizationRequest Data) : ICommand<int>;

/// <summary>
/// Хендлер команды для создания организации
/// </summary>
internal class CreateOrganizationCommandHandler : ICommandHandler<CreateOrganizationCommand, int>
{
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper  _mapper;

    public CreateOrganizationCommandHandler(IOrganizationRepository organizationRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _organizationRepository = organizationRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
    {
        var organization = _mapper.Map<Domain.Models.Organization>(request.Data);
        _organizationRepository.Create(organization);
        await _unitOfWork.CommitAsync(cancellationToken);
        return organization.Id;
    }
}