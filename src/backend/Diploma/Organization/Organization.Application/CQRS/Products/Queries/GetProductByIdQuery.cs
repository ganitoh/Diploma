using AutoMapper;
using Common.Application;
using Common.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Organization.Application.Commnon.Persistance;
using Organization.ApplicationContract.Dtos;

namespace Organizaiton.Application.CQRS.Products.Queries;

/// <summary>
/// Запрос для получения товара по идентификатору
/// </summary>
public record GetProductByIdQuery(int ProductId) : IQuery<ProductDto>;

/// <summary>
/// Хендлер запроса для получения товара по идентификатору
/// </summary>
internal class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductDto>
{
    private readonly IReadonlyOrganizationDbContext _dbContext;
    private readonly IMapper  _mapper;

    public GetProductByIdQueryHandler(IReadonlyOrganizationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products
                          .FirstOrDefaultAsync(x => x.Id==request.ProductId, cancellationToken) ?? throw new NotFoundException("Товар не найден");
        
        return _mapper.Map<ProductDto>(product);
    }
}