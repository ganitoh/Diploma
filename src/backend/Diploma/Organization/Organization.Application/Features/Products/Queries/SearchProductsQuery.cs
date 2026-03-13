using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Application;
using Microsoft.EntityFrameworkCore;
using Organizaiton.Application.Common.Persistance;
using Organization.ApplicationContract.Dtos;

namespace Organization.Application.Features.Products.Queries;

/// <summary>
/// Запрос на поиск продуктов
/// </summary>
public record class SearchProductsQuery(string SearchString) : IQuery<ICollection<ShortProductDto>>;

/// <summary>
/// Хендлер для поика продуктов
/// </summary>
internal class SearchProductsQueryHandler : IQueryHandler<SearchProductsQuery, ICollection<ShortProductDto> >
{
    private readonly IReadOnlyOrganizationDbContext  _context;
    private readonly IMapper _mapper;

    public SearchProductsQueryHandler(IReadOnlyOrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<ShortProductDto>> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Products
            .Where(x => x.Name.ToLower().Contains(request.SearchString.ToLower()) && x.IsStock)
            .ProjectTo<ShortProductDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}