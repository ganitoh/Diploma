using System.Security.Claims;
using System.Security.Principal;
using AutoMapper;
using Common.Application;
using Microsoft.EntityFrameworkCore;
using Organization.ApplicationContract.Requests;
using Organization.Domain.Models;
using Organization.Infrastructure.Persistance.Context;

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
                .FirstOrDefaultAsync(x => x.OrganizationUsers.Select(user => user.UserId).Contains(request.UserId),
                    cancellationToken);

            if (organizationByUserId is not null)
                throw new ApplicationException("Организаци уже существует");
        }
        
        var organization = _mapper.Map<Domain.Models.Organization>(request.OrganizationData);
        organization.Rating = new Rating();
        
        _context.Organizations.Add(organization);
        await _context.SaveChangesAsync(cancellationToken);
        
        organization.OrganizationUsers = new  List<OrganizationUser>
        {
            new() {UserId = request.UserId, OrganizationId = organization.Id}
        };
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return organization.Id;
    }
}