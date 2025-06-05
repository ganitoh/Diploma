using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Application;
using Microsoft.EntityFrameworkCore;
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
    private OrganizationDbContext  _context;
    private IMapper _mapper;

    public GetTopSellingProductsQueryHandler(OrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<ShortProductDto>> Handle(GetTopSellingProductsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Products
            .AsNoTracking()
            .OrderByDescending(x=>x.TotalSold).Take(request.Top)
            .ProjectTo<ShortProductDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}