using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.API.Paged;
using Common.Application;
using Microsoft.EntityFrameworkCore;
using Organization.ApplicationContract.Dtos;
using Organization.ApplicationContract.Requests;
using Organization.Infrastructure.Persistance.Context;

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
    private readonly OrganizationDbContext _context;
    private readonly IMapper  _mapper;

    public GetPagedProductsCommandHandler(OrganizationDbContext dbContext, IMapper mapper)
    {
        _context = dbContext;
        _mapper = mapper;
    }

    public async Task<PagedList<ProductDto>> Handle(GetPagedProductsCommand request, CancellationToken cancellationToken)
    {
        var productsQuery = _context.Products
            .Include(x => x.SellOrganization)
            .Include(x => x.Rating)
            .AsNoTracking();

        if (request.PagedRequest.OrganizationId is not null)
        {
            productsQuery =  productsQuery.Where(x => x.SellOrganizationId == request.PagedRequest.OrganizationId);
        }
        
        return await productsQuery
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .GetPagetListAsync(request.PagedRequest, cancellationToken);
    }
}