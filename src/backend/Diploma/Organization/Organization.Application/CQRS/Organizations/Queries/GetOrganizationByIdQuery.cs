using AutoMapper;
using Common.Application;
using Common.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Organization.ApplicationContract.Dtos;
using Organization.Infrastructure.Persistance.Context;

namespace Organizaiton.Application.CQRS.Organizations.Queries;

/// <summary>
/// Запрос на получение организации по иднтификатору
/// </summary>
public record GetOrganizationByIdQuery(int OrganizationId) : IQuery<OrganizationDto>;

/// <summary>
/// Хендлер запроса на получение организации по иднтификатору
/// </summary>
internal class GetOrganizationByIdQueryHandler : IQueryHandler<GetOrganizationByIdQuery, OrganizationDto>
{
    private readonly OrganizationDbContext _context;
    private readonly IMapper _mapper;

    public GetOrganizationByIdQueryHandler(OrganizationDbContext context, IMapper mapperl)
    {
        _context = context;
        _mapper = mapperl;
    }

    public async Task<OrganizationDto> Handle(GetOrganizationByIdQuery request, CancellationToken cancellationToken)
    {
        var organization = await _context
            .Organizations
            .AsNoTracking()
            .Include(x=>x.Products).ThenInclude(x=>x.Rating)
            .Include(x=>x.SellOrders)
            .Include(x=>x.BuyOrders)
            .FirstOrDefaultAsync(x=>x.Id == request.OrganizationId, cancellationToken) ?? throw new NotFoundException("Организация не найдена");
        
        return _mapper.Map<OrganizationDto>(organization);
    }
}