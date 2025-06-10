using Common.Application;
using Common.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Organization.Infrastructure.Persistance.Context;

namespace Organization.Application.CQRS.Organizations.Commands;

/// <summary>
/// Команда для верификации организации админом
/// </summary>
public record VerifyOrganizationCommand(int OrganizationId) : ICommand<Unit>;

/// <inheritdoc />
internal class VerifyOrganizationCommandHandler : ICommandHandler<VerifyOrganizationCommand, Unit>
{
    private readonly OrganizationDbContext  _context;

    public VerifyOrganizationCommandHandler(OrganizationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(VerifyOrganizationCommand request, CancellationToken cancellationToken)
    {
        var organization =
            await _context.Organizations.FirstOrDefaultAsync(x => x.Id == request.OrganizationId, cancellationToken) ??
            throw new NotFoundException("Организация не найдена");

        organization.IsApproval = true;
        
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}