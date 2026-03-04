using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Application;
using Microsoft.EntityFrameworkCore;
using Organizaiton.Application.Common.Persistance;
using Organization.ApplicationContract.Dtos;

namespace Organizaiton.Application.CQRS.Organizations.Queries;

/// <summary>
/// Получить топ организаций по рейтингу
/// </summary>
public record class GetTopOrganizationByRatingQuery(int Top) : IQuery<ICollection<ShortOrganizationDto>>;

internal class GetTopOrganizationByRatingQueryHandler : IQueryHandler<GetTopOrganizationByRatingQuery, ICollection<ShortOrganizationDto>>
{
    private readonly IReadOnlyOrganizationDbContext _context;
    private readonly IMapper _mapper;

    public GetTopOrganizationByRatingQueryHandler(IReadOnlyOrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<ShortOrganizationDto>> Handle(GetTopOrganizationByRatingQuery request, CancellationToken cancellationToken)
    {
        return await _context.Organizations
            .OrderByDescending(x=>x.Rating.Value)
            .Take(request.Top)
            .ProjectTo<ShortOrganizationDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}