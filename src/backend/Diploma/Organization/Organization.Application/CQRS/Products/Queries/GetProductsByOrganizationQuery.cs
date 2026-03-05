using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Application;
using Microsoft.EntityFrameworkCore;
using Organizaiton.Application.Common.Persistance;
using Organization.ApplicationContract.Dtos;

namespace Organizaiton.Application.CQRS.Products.Queries;

/// <summary>
/// Получить товаря для организации (сокращенные данные)
/// </summary>
public record GetProductsByOrganizationQuery(int OrganizationId) : IQuery<ICollection<ShortProductDto>>;

/// <inheritdoc />
internal class GetProductsByOrganizationQueryHandler : IQueryHandler<GetProductsByOrganizationQuery,
    ICollection<ShortProductDto>>
{
    private readonly IReadOnlyOrganizationDbContext _context;
    private readonly IMapper _mapper;

    public GetProductsByOrganizationQueryHandler(IReadOnlyOrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<ShortProductDto>> Handle(GetProductsByOrganizationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Products
            .Where(x => x.OrganizationId == request.OrganizationId)
            .ProjectTo<ShortProductDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}