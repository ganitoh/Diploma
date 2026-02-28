using AutoMapper;
using Common.Application;
using Organization.ApplicationContract.Requests;
using Organization.Domain.ValueObjects;

namespace Organization.Application.CQRS.Organizations.Commands;

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
    private readonly OrganizationDbContext _context;
    private readonly IMapper  _mapper;

    public CreateOrganizationCommandHandler(
        OrganizationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
    {
        if (!request.OrganizationData.IsExternal)
        {
            var organizationByUserId = await _context.Organizations
                .Include(x => x.OrganizationUsers)
                .FirstOrDefaultAsync(x => x.OrganizationUsers.Select(user => user.UserId)
                        .Contains(request.UserId),
                    cancellationToken);

            if (organizationByUserId is not null)
                throw new ApplicationException("Организаця уже существует");
        }

        var address = _mapper.Map<Address>(request.OrganizationData.LegalAddress);
        
        var organization = new Organization.Domain.Models.Organization(request.OrganizationData.Name, request.OrganizationData.Inn, address);
        organization.AddUser(request.UserId);
        
        _context.Organizations.Add(organization);
        await _context.SaveChangesAsync(cancellationToken);

        
        await _context.SaveChangesAsync(cancellationToken);
        
        return organization.Id;
    }
}