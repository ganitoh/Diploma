using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.API.Paged;
using Common.Application;
using Microsoft.EntityFrameworkCore;
using Organizaiton.Application.Common.Persistance;
using Organization.ApplicationContract.Dtos;
using Organization.ApplicationContract.Requests;

namespace Organization.Application.Features.Organizations.Queries;

/// <summary>
/// Запрос на получение пагинированного списка организаций
/// </summary>
public record GetPagedOrganizationQuery(GetPagedOrganizationsRequest Data) : IQuery<PagedList<OrganizationDto>>;

/// <summary>
/// Хендлер запроса на получение пагинированного списка организаций
/// </summary>
internal class GetPagedOrganizationQueryHandler : IQueryHandler<GetPagedOrganizationQuery, PagedList<OrganizationDto>>
{
    private readonly IReadOnlyOrganizationDbContext _context;
    private readonly IMapper _mapper;

    public GetPagedOrganizationQueryHandler(IReadOnlyOrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedList<OrganizationDto>> Handle(GetPagedOrganizationQuery request, CancellationToken cancellationToken)
    {
        var pagedListOrganization = await _context.Organizations
            .PagedQueryable(request.Data.PageNumber, request.Data.PageSize)
            .ProjectTo<OrganizationDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        var totalCount = await _context.Organizations.CountAsync(cancellationToken);
        
        return new PagedList<OrganizationDto>(pagedListOrganization, totalCount);
    }
}