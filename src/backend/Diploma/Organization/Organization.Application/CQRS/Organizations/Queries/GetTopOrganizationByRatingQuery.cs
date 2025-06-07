using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Application;
using Microsoft.EntityFrameworkCore;
using Organization.ApplicationContract.Dtos;
using Organization.Infrastructure.Persistance.Context;

namespace Organizaiton.Application.CQRS.Organizations.Queries;

/// <summary>
/// Получить топ организаций по рейтингу
/// </summary>
public record class GetTopOrganizationByRatingQuery(int Top) : IQuery<ICollection<ShortOrganizationDto>>;

internal class GetTopOrganizationByRatingQueryHandler : IQueryHandler<GetTopOrganizationByRatingQuery, ICollection<ShortOrganizationDto>>
{
    private readonly OrganizationDbContext  _context;
    private readonly IMapper _mapper;

    public GetTopOrganizationByRatingQueryHandler(OrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<ShortOrganizationDto>> Handle(GetTopOrganizationByRatingQuery request, CancellationToken cancellationToken)
    {
        return await _context.Organizations
            .AsNoTracking()
            .Include(x=>x.Rating)
            .OrderByDescending(x=>x.Rating.Vale)
            .Take(request.Top).ProjectTo<ShortOrganizationDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}