using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Application;
using Common.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Organizaiton.Application.Common.Persistance;
using Organization.ApplicationContract.Dtos;

namespace Organizaiton.Application.CQRS.Ratings.Queries;

/// <summary>
/// Запрос на получение отзвов
/// </summary>
public record GetRatingsQuery(int RatingId) : IQuery<RatingDto>;

/// <inheritdoc />
internal class GetRatingsQueryHandler : IQueryHandler<GetRatingsQuery, RatingDto>
{
    private readonly IReadOnlyOrganizationDbContext _context;
    private readonly IMapper _mapper;

    public GetRatingsQueryHandler(IReadOnlyOrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<RatingDto> Handle(GetRatingsQuery request, CancellationToken cancellationToken)
    {
         var result =  await _context.Ratings
             .ProjectTo<RatingDto>(_mapper.ConfigurationProvider)
             .FirstOrDefaultAsync(x=>x.Id == request.RatingId, cancellationToken);

         if (result is null)
             throw new NotFoundException("Рейтинг не найден");
         
         return result;
    }
}
