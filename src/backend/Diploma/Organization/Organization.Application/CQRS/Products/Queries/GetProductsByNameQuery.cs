using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Application;
using Microsoft.EntityFrameworkCore;
using Organization.Application.Common.Persistance;
using Organization.ApplicationContract.Dtos;

namespace Organization.Application.CQRS.Products.Queries;

/// <summary>
/// Поиск товара по названию
/// </summary>
/// <param name="NameText"></param>
public record class GetProductsByNameQuery(string NameText) : IQuery<ICollection<ProductDto>>;

/// <summary>
/// Хендлер  для поиска товара по названию
/// </summary>
class GetProductsByNameQueryHandler : IQueryHandler<GetProductsByNameQuery, ICollection<ProductDto>>
{
    private readonly IReadOnlyOrganizationDbContext _context;
    private readonly IMapper _mapper;

    public GetProductsByNameQueryHandler(IReadOnlyOrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<ProductDto>> Handle(GetProductsByNameQuery request, CancellationToken cancellationToken)
    {
        var products = await _context.Products
            .Where(x => x.Name.Contains(request.NameText))
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return products;
    }
}