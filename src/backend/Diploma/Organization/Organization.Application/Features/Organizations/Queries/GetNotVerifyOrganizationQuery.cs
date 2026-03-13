using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Application;
using Microsoft.EntityFrameworkCore;
using Organizaiton.Application.Common.Persistance;
using Organization.ApplicationContract.Dtos;

namespace Organization.Application.Features.Organizations.Queries;

/// <summary>
/// Запрос на получение непроверенных организаций
/// </summary>
public class GetNotVerifyOrganizationQuery : IQuery<ICollection<OrganizationDto>>;

/// <inheritdoc />
internal class GetNotVerifyOrganizationQueryHandler : IQueryHandler<GetNotVerifyOrganizationQuery,  ICollection<OrganizationDto>>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyOrganizationDbContext _context;

    public GetNotVerifyOrganizationQueryHandler(IMapper mapper, IReadOnlyOrganizationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ICollection<OrganizationDto>> Handle(GetNotVerifyOrganizationQuery request, CancellationToken cancellationToken)
    {
        var organizations = await _context.Organizations
            .Where(x => !x.IsApproval)
            .ProjectTo<OrganizationDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        return organizations;
    }
}