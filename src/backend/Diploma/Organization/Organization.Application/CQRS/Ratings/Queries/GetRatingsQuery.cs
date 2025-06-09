using AutoMapper;
using Common.Application;
using Common.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Organization.ApplicationContract.Dtos;
using Organization.Infrastructure.Persistance.Context;

namespace Organizaiton.Application.CQRS.Ratings.Queries;

/// <summary>
/// Запрос на получение отзвов
/// </summary>
/// <param name="EntityId">Идентификатор сущности</param>
/// <param name="IsProduct">Флаг - для чего надо получить отзывы (продукт/организация)</param>
public record GetRatingsQuery(int EntityId, bool IsProduct) : IQuery<RatingDto>;

/// <inheritdoc />
internal class GetRatingsQueryHandler : IQueryHandler<GetRatingsQuery, RatingDto>
{
    private readonly IMapper _mapper;
    private readonly OrganizationDbContext _context;

    public GetRatingsQueryHandler(IMapper mapper, OrganizationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<RatingDto> Handle(GetRatingsQuery request, CancellationToken cancellationToken)
    {
        RatingDto ratingResult = null;

        if (request.IsProduct)
        {
            var product = await _context.Products
                .AsNoTracking()
                .Include(x => x.Rating)
                .ThenInclude(x => x.Commentaries)
                .FirstOrDefaultAsync(x => x.Id == request.EntityId, cancellationToken) 
                          ?? throw new NotFoundException("Товар не найден");
            
            ratingResult = _mapper.Map<RatingDto>(product.Rating);
        }
        else
        {
            var organization = await _context.Products
                              .AsNoTracking()
                              .Include(x => x.Rating)
                              .ThenInclude(x => x.Commentaries)
                              .FirstOrDefaultAsync(x => x.Id == request.EntityId, cancellationToken) 
                          ?? throw new NotFoundException("Организация не найдена");
            
            ratingResult = _mapper.Map<RatingDto>(organization.Rating);
        }
        
        return ratingResult;
    }
}
