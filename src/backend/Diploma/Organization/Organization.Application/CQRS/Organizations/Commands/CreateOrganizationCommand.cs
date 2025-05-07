using System.Security.Claims;
using AutoMapper;
using Common.Application;
using Common.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Organization.Application.Commnon.Persistance;
using Organization.ApplicationContract.Requests;
using Organization.Application.Commnon.Persistance.Repositories;
using Organization.Domain.Models;

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
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IReadonlyOrganizationDbContext _context;
    private readonly IHttpContextAccessor _httpContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper  _mapper;

    public CreateOrganizationCommandHandler(
        IOrganizationRepository organizationRepository,
        IReadonlyOrganizationDbContext readonlyOrganizationDbContext,
        IHttpContextAccessor httpContext,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _organizationRepository = organizationRepository;
        _context = readonlyOrganizationDbContext;
        _httpContext = httpContext;
        _unitOfWork = unitOfWork;
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
        
        _organizationRepository.Create(organization);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        organization.OrganizationUsers = new  List<OrganizationUser>
        {
            new() {UserId = Guid.Parse(userId), OrganizationId = organization.Id}
        };
        
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return organization.Id;
    }
}