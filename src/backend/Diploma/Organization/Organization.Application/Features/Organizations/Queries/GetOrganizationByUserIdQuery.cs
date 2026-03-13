using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Application;
using Common.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Organizaiton.Application.Common.Persistance;
using Organization.ApplicationContract.Dtos;

namespace Organization.Application.Features.Organizations.Queries;

/// <summary>
/// Получить организацию по идентификатору пользователя
/// </summary>
public record GetOrganizationByUserIdQuery(Guid UserId) : IQuery<OrganizationDto>;

/// <summary>
/// Хандлер получения организацию по идентификатору пользователя
/// </summary>
internal class GetOrganizationByUserIdQueryHandler : IQueryHandler<GetOrganizationByUserIdQuery, OrganizationDto>
{
    private readonly IReadOnlyOrganizationDbContext _context;
    private readonly IMapper _mapper;

    public GetOrganizationByUserIdQueryHandler(IReadOnlyOrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OrganizationDto> Handle(GetOrganizationByUserIdQuery request, CancellationToken cancellationToken)
    {
        var organization = await _context.Organizations
            .ProjectTo<OrganizationDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.UserId == request.UserId.ToString(), cancellationToken);

        if (organization is null)
            throw new NotFoundException("Организация не найдена");
        
        return _mapper.Map<OrganizationDto>(organization);
    }
}