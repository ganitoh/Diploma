using AutoMapper;
using Common.Application;
using Common.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Organization.ApplicationContract.Dtos;
using Organization.Infrastructure.Persistance.Context;

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
    private readonly OrganizationDbContext _context;
    private readonly IMapper  _mapper;

    public GetProductByIdQueryHandler(OrganizationDbContext dbContext, IMapper mapper)
    {
        _context = dbContext;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .AsNoTracking()
            .Include(x=>x.SellOrganization)
            .Include(x=>x.Rating)
            .FirstOrDefaultAsync(x => x.Id==request.ProductId, cancellationToken) 
                      ?? throw new NotFoundException("Товар не найден");
        
        return _mapper.Map<ProductDto>(product);
    }
}