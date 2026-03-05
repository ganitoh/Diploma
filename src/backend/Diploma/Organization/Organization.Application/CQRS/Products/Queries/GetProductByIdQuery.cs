using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Application;
using Common.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Organizaiton.Application.Common.Persistance;
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
    private readonly IReadOnlyOrganizationDbContext _context;
    private readonly IMapper  _mapper;

    public GetProductByIdQueryHandler(IReadOnlyOrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken) ?? throw new NotFoundException("Товар не найден");
        
        return product;
    }
}