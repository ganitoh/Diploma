using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Application;
using Microsoft.EntityFrameworkCore;
using Organization.ApplicationContract.Dtos;
using Organization.Infrastructure.Persistance.Context;

namespace Organizaiton.Application.CQRS.Products.Queries;

/// <summary>
/// Запрос на поиск продуктов
/// </summary>
public record class SearchProductsQuery(string SearchString) : IQuery<ICollection<ShortProductDto>>;

/// <summary>
/// Хендлер для поика продуктов
/// </summary>
internal class SearchProductsQueryHandler : IQueryHandler<SearchProductsQuery, ICollection<ShortProductDto> >
{
    private readonly OrganizationDbContext  _context;
    private readonly IMapper _mapper;

    public SearchProductsQueryHandler(OrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<ShortProductDto>> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Products
            .AsNoTracking()
            .Where(x => x.Name.Contains(request.SearchString) && x.IsStock)
            .ProjectTo<ShortProductDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}