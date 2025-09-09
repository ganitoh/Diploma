using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.API.Paged;
using Common.Application;
using Microsoft.EntityFrameworkCore;
using Organization.ApplicationContract.Dtos;
using Organization.ApplicationContract.Requests;
using Organization.Infrastructure.Persistance.Context;

namespace Organizaiton.Application.CQRS.Organizations.Queries;

/// <summary>
/// Запрос на получение пагинированного списка организаций
/// </summary>
public record GetPagedOrganizationQuery(GetPagedOrganizationsRequest Data) : IQuery<PagedList<OrganizationDto>>;

/// <summary>
/// Хендлер запроса на получение пагинированного списка организаций
/// </summary>
internal class GetPagedOrganizationQueryHandler : IQueryHandler<GetPagedOrganizationQuery, PagedList<OrganizationDto>>
{
    private readonly OrganizationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetPagedOrganizationQueryHandler(IMapper mapper, OrganizationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<PagedList<OrganizationDto>> Handle(GetPagedOrganizationQuery request, CancellationToken cancellationToken)
    {
        var pagedListOrganization = await _dbContext.Organizations
            .AsNoTracking()
            .Include(x => x.OrganizationUsers)
            .Where(x=> x.IsExternal == request.Data.IsExternal)
            .PagedQueryable(request.Data.PageNumber, request.Data.PageSize)
            .ProjectTo<OrganizationDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        var totalCount = await _dbContext.Organizations.CountAsync(cancellationToken);
        
        return new PagedList<OrganizationDto>(pagedListOrganization, totalCount);
    }
}