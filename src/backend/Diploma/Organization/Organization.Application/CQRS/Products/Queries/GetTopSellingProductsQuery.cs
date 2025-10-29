using System.Text.Json;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Organization.ApplicationContract.Dtos;
using Organization.Infrastructure.Persistance.Context;

namespace Organizaiton.Application.CQRS.Products.Queries;

/// <summary>
/// Получить топ продоваемых товаров
/// </summary>
public record class GetTopSellingProductsQuery(int Top) : IQuery<ICollection<ShortProductDto>>;

/// <summary>
/// Хандлер для получения топ продоваемых товаров
/// </summary>
internal class GetTopSellingProductsQueryHandler : IQueryHandler<GetTopSellingProductsQuery, ICollection<ShortProductDto>>
{
    private const string CacheKey = "TopSellingProducts";
    
    private OrganizationDbContext  _context;
    private IMapper _mapper;
    private IDistributedCache _cache;

    public GetTopSellingProductsQueryHandler(OrganizationDbContext context, IMapper mapper, IDistributedCache cache)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<ICollection<ShortProductDto>> Handle(GetTopSellingProductsQuery request, CancellationToken cancellationToken)
    {
        var cachedTopSellingProducts = await _cache.GetStringAsync(CacheKey, cancellationToken);
        
        if (string.IsNullOrEmpty(cachedTopSellingProducts))
        {
            var response = await _context.Products
                .AsNoTracking()
                .Include(x=>x.Rating)
                .OrderByDescending(x=>x.TotalSold)
                .Take(request.Top)
                .ProjectTo<ShortProductDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if (response.Count > 0)
            { 
                var topSellingPriceString = JsonSerializer.Serialize(response);
                await _cache.SetStringAsync(CacheKey, topSellingPriceString, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
                }, cancellationToken);
            }

            return response;
        }
        else
        {
            var response = JsonSerializer.Deserialize<List<ShortProductDto>>(cachedTopSellingProducts);
            return response ?? throw new ApplicationException($"{nameof(GetTopSellingProductsQuery)} returned null");
        }
    }
}