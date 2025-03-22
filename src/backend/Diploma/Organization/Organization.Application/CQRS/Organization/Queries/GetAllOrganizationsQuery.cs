using System.Text.Json;
using AutoMapper;
using Common.API.Paged;
using Common.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Organization.Application.Common.Persistance;
using Organization.ApplicationContract.Dtos;

namespace Organization.Application.CQRS.Organization.Queries;

/// <summary>
/// Получить все организации
/// </summary>
/// <param name="QueryData"></param>
public record class GetAllOrganizationsQuery(PagedRequest QueryData) : IQuery<PagedList<OrganizationDto>>;

/// <summary>
/// Хендлер для получения всех организаций
/// </summary>
internal class GetAllOrganizationsQueryHandler : IQueryHandler<GetAllOrganizationsQuery,  PagedList<OrganizationDto>>
{
    private const string FirstTenCashedOrganizationKey = "FirstTenCashedOrganization";
    
    private readonly IReadOnlyOrganizationDbContext _context;
    private readonly IDistributedCache _redisCache;
    private readonly IMapper _mapper;

    public GetAllOrganizationsQueryHandler(
        IReadOnlyOrganizationDbContext context, 
        IDistributedCache redisCache,
        IMapper mapper)
    {
        _context = context;
        _redisCache = redisCache;
        _mapper = mapper;
    }

    public async Task<PagedList<OrganizationDto>> Handle(GetAllOrganizationsQuery request, CancellationToken cancellationToken)
    {
        var organizationsDtoString = await _redisCache.GetStringAsync(FirstTenCashedOrganizationKey,cancellationToken);
        PagedList<OrganizationDto> result;
        if (string.IsNullOrEmpty(organizationsDtoString))
        {
            var organizationsTotalCount = await _context.Organizations.CountAsync(cancellationToken);
        
            var organizations = _context.Organizations.PagedQueryable(request.QueryData.PageNumber, request.QueryData.PageSize).ToListAsync(cancellationToken);
            var organizationsDto = _mapper.Map<List<OrganizationDto>>(organizations);
            result = new PagedList<OrganizationDto>(organizationsDto, organizationsTotalCount);
            await _redisCache.SetStringAsync("FirstTenCashedOrganization", JsonSerializer.Serialize(result),cancellationToken);
        }
        else
        {
            result = JsonSerializer.Deserialize<PagedList<OrganizationDto>>(organizationsDtoString)!;
        }
        
        return result;
    }
}