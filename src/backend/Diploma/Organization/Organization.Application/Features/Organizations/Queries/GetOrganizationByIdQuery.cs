using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Application;
using Common.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Organizaiton.Application.Common.Persistance;
using Organization.ApplicationContract.Dtos;

namespace Organization.Application.Features.Organizations.Queries;

/// <summary>
/// Запрос на получение организации по иднтификатору
/// </summary>
public record GetOrganizationByIdQuery(int OrganizationId) : IQuery<OrganizationDto>;

/// <summary>
/// Хендлер запроса на получение организации по иднтификатору
/// </summary>
internal class GetOrganizationByIdQueryHandler : IQueryHandler<GetOrganizationByIdQuery, OrganizationDto>
{
    private readonly IReadOnlyOrganizationDbContext _context;
    private readonly IMapper _mapper;

    public GetOrganizationByIdQueryHandler(IReadOnlyOrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OrganizationDto> Handle(GetOrganizationByIdQuery request, CancellationToken cancellationToken)
    {
        var organization = await _context.Organizations
            .ProjectTo<OrganizationDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == request.OrganizationId, cancellationToken);

        if (organization is null)
            throw new NotFoundException("Организация не найдена");
        
        return organization;
    }
}