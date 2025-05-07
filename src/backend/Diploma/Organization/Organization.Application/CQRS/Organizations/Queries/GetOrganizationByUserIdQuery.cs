using AutoMapper;
using Common.Application;
using Common.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Organization.Application.Commnon.Persistance;
using Organization.ApplicationContract.Dtos;

namespace Organizaiton.Application.CQRS.Organizations.Queries;

/// <summary>
/// Получить организацию по идентификатору пользователя
/// </summary>
public record GetOrganizationByUserIdQuery(Guid UserId) : IQuery<OrganizationDto>;

/// <summary>
/// Хандлер получения организацию по идентификатору пользователя
/// </summary>
internal class GetOrganizationByUserIdQueryHandler : IQueryHandler<GetOrganizationByUserIdQuery, OrganizationDto>
{
    private readonly IReadonlyOrganizationDbContext _context;
    private readonly IMapper _mapper;

    public GetOrganizationByUserIdQueryHandler(IReadonlyOrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OrganizationDto> Handle(GetOrganizationByUserIdQuery request, CancellationToken cancellationToken)
    {
        var organization = await _context.Organizations
            .AsNoTracking()
            .Include(x=>x.OrganizationUsers)
            .FirstOrDefaultAsync(x=>x.OrganizationUsers.Select(user => user.UserId)
                .Contains(request.UserId), cancellationToken);

        if (organization is null)
            throw new NotFoundException("Организация не найдена");
        
        return _mapper.Map<OrganizationDto>(organization);
    }
}