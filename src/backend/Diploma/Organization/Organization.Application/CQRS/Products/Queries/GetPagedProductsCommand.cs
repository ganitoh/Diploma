using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.API.Paged;
using Common.Application;
using Organizaiton.Application.Common.Persistance;
using Organization.ApplicationContract.Dtos;
using Organization.ApplicationContract.Requests;

namespace Organizaiton.Application.CQRS.Products.Queries;

/// <summary>
/// Запрос на получение пагинированного списка продуктов
/// </summary>
public record GetPagedProductsCommand(GetPagedProductsRequest PagedRequest) : IQuery<PagedList<ProductDto>>;

/// <summary>
/// Хендлер запроса на получение пагинированного списка продуктов
/// </summary>
internal class GetPagedProductsCommandHandler : IQueryHandler<GetPagedProductsCommand, PagedList<ProductDto>>
{
    private readonly IReadOnlyOrganizationDbContext _context;
    private readonly IMapper  _mapper;

    public GetPagedProductsCommandHandler(IReadOnlyOrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedList<ProductDto>> Handle(GetPagedProductsCommand request, CancellationToken cancellationToken)
    {
        var productsQuery = _context.Products;

        if (request.PagedRequest.OrganizationId is not null)
        {
            productsQuery =  productsQuery.Where(x => x.OrganizationId == request.PagedRequest.OrganizationId);
        }
        
        return await productsQuery
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .GetPagetListAsync(request.PagedRequest, cancellationToken);
    }
}