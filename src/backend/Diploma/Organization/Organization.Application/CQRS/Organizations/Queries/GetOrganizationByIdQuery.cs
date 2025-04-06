using AutoMapper;
using Common.Application;
using Common.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Organization.ApplicationContract.Dtos;
using Organization.Application.Commnon.Persistance;

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
    private readonly IReadonlyOrganizationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetOrganizationByIdQueryHandler(IReadonlyOrganizationDbContext dbContext, IMapper mapperl)
    {
        _dbContext = dbContext;
        _mapper = mapperl;
    }

    public async Task<OrganizationDto> Handle(GetOrganizationByIdQuery request, CancellationToken cancellationToken)
    {
        var organization = await _dbContext
            .Organizations
            .Include(x=>x.Products)
            .Include(x=>x.SellOrders)
            .Include(x=>x.BuyOrders)
            .FirstOrDefaultAsync(x=>x.Id == request.OrganizationId, cancellationToken) ?? throw new NotFoundException("Организация не найдена");
        
        return _mapper.Map<OrganizationDto>(organization);
    }
}