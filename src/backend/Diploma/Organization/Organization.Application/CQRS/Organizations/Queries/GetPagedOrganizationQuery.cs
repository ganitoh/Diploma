using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.API.Paged;
using Common.Application;
using Microsoft.EntityFrameworkCore;
using Organization.ApplicationContract.Dtos;
using Organization.Application.Commnon.Persistance;

namespace Organizaiton.Application.CQRS.Organizations.Queries;

/// <summary>
/// Запрос на получение пагинированного списка организаций
/// </summary>
public record GetPagedOrganizationQuery(PagedRequest RequestData) : IQuery<PagedList<OrganizationDto>>;

/// <summary>
/// Хендлер запроса на получение пагинированного списка организаций
/// </summary>
internal class GetPagedOrganizationQueryHandler : IQueryHandler<GetPagedOrganizationQuery, PagedList<OrganizationDto>>
{
    private readonly IMapper _mapper;
    private readonly IReadonlyOrganizationDbContext _dbContext;

    public GetPagedOrganizationQueryHandler(IMapper mapper, IReadonlyOrganizationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<PagedList<OrganizationDto>> Handle(GetPagedOrganizationQuery request, CancellationToken cancellationToken)
    {
        var pagedListOrganization = await _dbContext.Organizations
            .PagedQueryable(request.RequestData.PageNumber, request.RequestData.PageSize)
            .ProjectTo<OrganizationDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        var totalCount = await _dbContext.Organizations.CountAsync(cancellationToken);
        
        return new PagedList<OrganizationDto>(pagedListOrganization, totalCount);
    }
}