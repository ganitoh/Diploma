using Common.Application;
using Common.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Organization.ApplicationContract.Requests;
using Organization.Infrastructure.Persistance.Context;

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
    private readonly OrganizationDbContext _context;

    public UpdateOrganizationDataCommandHandler(OrganizationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(UpdateOrganizationDataCommand request, CancellationToken cancellationToken)
    {
        var organization =
            await _context.Organizations.FirstOrDefaultAsync(x => x.Id == request.RequestData.OrganizationId,
                cancellationToken);
        
        if (organization is null)
            throw new NotFoundException("Организация не найдена");
                           
        
        organization.Name = request.RequestData.Name;
        organization.Email = request.RequestData.Email;
        organization.Description = request.RequestData.Description;
        organization.Inn = request.RequestData.Inn;
        organization.LegalAddress = request.RequestData.LegalAddress;
        organization.IsApproval = false;
        
        await _context.SaveChangesAsync(cancellationToken);
        return organization.Id;
    }
}