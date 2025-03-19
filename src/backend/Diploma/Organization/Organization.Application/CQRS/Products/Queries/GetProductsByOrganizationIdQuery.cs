using AutoMapper;
using Common.API.Paged;
using Common.Application;
using Microsoft.EntityFrameworkCore;
using Organization.Application.Common.Persistance;
using Organization.ApplicationContract.Dtos;
using Organization.Domain.Models;

namespace Organization.Application.CQRS.Products.Queries;

/// <summary>
/// Запрос на получение товаров организации
/// </summary>
public record class GetProductsByOrganizationIdQuery(PagedRequest PagedRequest, int OrganizationId) : IQuery<PagedList<ProductDto>>;

/// <summary>
/// Хендлер запроса на получение товаров организации
/// </summary>
internal class GetProductsByOrganizationIdQueryHandler : IQueryHandler<GetProductsByOrganizationIdQuery, PagedList<ProductDto>>
{
    private readonly IReadOnlyOrganizationDbContext _context;
    private readonly IMapper  _mapper;

    public GetProductsByOrganizationIdQueryHandler(IReadOnlyOrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedList<ProductDto>> Handle(GetProductsByOrganizationIdQuery request, CancellationToken cancellationToken)
    {
        var products =  await _context.Products
            .Where(x=>x.OrganizationId == request.OrganizationId)
            .OrderBy(x => x.Id)
            .ToListAsync(cancellationToken);

        var resultProduct = products.PagedEnumerable(request.PagedRequest.PageNumber, request.PagedRequest.PageSize);
        
        return new PagedList<ProductDto>(_mapper.Map<IEnumerable<Product>, ICollection<ProductDto>>(resultProduct), products.Count);
    }
}