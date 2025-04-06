using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.API.Paged;
using Common.Application;
using Organization.Application.Commnon.Persistance;
using Organization.ApplicationContract.Dtos;

namespace Organizaiton.Application.CQRS.Products.Queries;

/// <summary>
/// Запрос на получение пагинированного списка продуктов
/// </summary>
public record GetPagedProductsCommand(PagedRequest PagedRequest) : IQuery<PagedList<ProductDto>>;

/// <summary>
/// Хендлер запроса на получение пагинированного списка продуктов
/// </summary>
internal class GetPagedProductsCommandHandler : IQueryHandler<GetPagedProductsCommand, PagedList<ProductDto>>
{
    private readonly IReadonlyOrganizationDbContext _dbContext;
    private readonly IMapper  _mapper;

    public GetPagedProductsCommandHandler(IReadonlyOrganizationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<PagedList<ProductDto>> Handle(GetPagedProductsCommand request, CancellationToken cancellationToken)
    {
        return await _dbContext.Products
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .GetPagetListAsync(request.PagedRequest, cancellationToken);
    }
}