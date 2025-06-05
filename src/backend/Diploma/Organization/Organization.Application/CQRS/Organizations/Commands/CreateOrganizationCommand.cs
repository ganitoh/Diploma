using System.Security.Claims;
using AutoMapper;
using Common.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Organization.ApplicationContract.Requests;
using Organization.Domain.Models;
using Organization.Infrastructure.Persistance.Context;

namespace Organization.Application.CQRS.Organizations.Commands;

/// <summary>
/// Команда для создания организации
/// </summary>
/// <param name="OrganizaitonData"></param>
public record CreateOrganizationCommand(CreateOrganizationRequest OrganizaitonData) : ICommand<int>;

/// <summary>
/// Хендлер команды для создания организации
/// </summary>
internal class CreateOrganizationCommandHandler : ICommandHandler<CreateOrganizationCommand, int>
{
    private readonly OrganizationDbContext _context;
    private readonly IHttpContextAccessor _httpContext;
    private readonly IMapper  _mapper;

    public CreateOrganizationCommandHandler(
        OrganizationDbContext context,
        IHttpContextAccessor httpContext,
        IMapper mapper)
    {
        _context = context;
        _httpContext = httpContext;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
    {
        var userId =
            _httpContext.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ??
            throw new ApplicationException("Идентификатор пользователя не нйден");

        var organizationByUserId = await _context.Organizations
            .Include(x => x.OrganizationUsers)
            .FirstOrDefaultAsync(x => x.OrganizationUsers.Select(user => user.UserId).Contains(Guid.Parse(userId)),
                cancellationToken);

        if (organizationByUserId is not null)
            throw new ApplicationException("Организаци уже существует");
        
        var organization = _mapper.Map<Domain.Models.Organization>(request.OrganizaitonData);
        
        _context.Organizations.Add(organization);
        await _context.SaveChangesAsync(cancellationToken);
        
        organization.OrganizationUsers = new  List<OrganizationUser>
        {
            new() {UserId = Guid.Parse(userId), OrganizationId = organization.Id}
        };
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return organization.Id;
    }
}