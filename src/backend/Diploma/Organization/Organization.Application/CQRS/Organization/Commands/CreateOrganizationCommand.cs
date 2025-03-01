using AutoMapper;
using Common.Application;
using Common.Infrastructure.UnitOfWork;
using Organization.Application.Common.Persistance.Repositories;
using Organization.ApplicationContract.Reqeusts;

namespace Organization.Application.CQRS.Organization.Commands;

public class CreateOrganizationCommand : CreateOrganizationRequest, ICommand<int>;

internal class CreateOrganizationCommandHandler : ICommandHandler<CreateOrganizationCommand, int>
{
    private readonly IOrganizationRepository  _organizationRepository;
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
        var organization = _mapper.Map<Domain.Models.Organization>(request);
        
        _organizationRepository.Create(organization);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return organization.Id;
    }
}