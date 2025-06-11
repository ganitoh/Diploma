using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Application;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Organization.ApplicationContract.Dtos;
using Organization.Infrastructure.Persistance.Context;

namespace Organizaiton.Application.CQRS.Products.Queries;

/// <summary>
/// Получить товаря для организации (сокращенные данные)
/// </summary>
public record GetProductsByOrganizationQuery(int OrganizationId) : IQuery<ICollection<ShortProductDto>>;

/// <inheritdoc />
internal class GetProductsByOrganizationQueryHandler : IQueryHandler<GetProductsByOrganizationQuery,
    ICollection<ShortProductDto>>
{
    private readonly OrganizationDbContext  _context;
    private readonly IMapper _mapper;

    public GetProductsByOrganizationQueryHandler(OrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<ShortProductDto>> Handle(GetProductsByOrganizationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Products
            .AsNoTracking()
            .Where(x=>x.SellOrganizationId == request.OrganizationId)
            .ProjectTo<ShortProductDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}