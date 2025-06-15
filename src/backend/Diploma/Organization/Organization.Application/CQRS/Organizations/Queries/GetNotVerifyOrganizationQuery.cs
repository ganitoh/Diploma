using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Application;
using Microsoft.EntityFrameworkCore;
using Organization.ApplicationContract.Dtos;
using Organization.Infrastructure.Persistance.Context;

namespace Organizaiton.Application.CQRS.Organizations.Queries;

/// <summary>
/// Запрос на получение непровеенных организаций
/// </summary>
public class GetNotVerifyOrganizationQuery : IQuery<ICollection<OrganizationDto>>;

/// <inheritdoc />
internal class GetNotVerifyOrganizationQueryHandler : IQueryHandler<GetNotVerifyOrganizationQuery,  ICollection<OrganizationDto>>
{
    private readonly IMapper _mapper;
    private readonly OrganizationDbContext _context;

    public GetNotVerifyOrganizationQueryHandler(IMapper mapper, OrganizationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ICollection<OrganizationDto>> Handle(GetNotVerifyOrganizationQuery request, CancellationToken cancellationToken)
    {
        var organizations = await _context.Organizations
            .AsNoTracking()
            .Include(x => x.OrganizationUsers)
            .Where(x => !x.IsApproval)
            .ProjectTo<OrganizationDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        return organizations;
    }
}