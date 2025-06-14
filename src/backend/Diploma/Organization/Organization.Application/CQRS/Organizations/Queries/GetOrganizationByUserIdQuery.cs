﻿using AutoMapper;
using Common.Application;
using Common.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Organization.ApplicationContract.Dtos;
using Organization.Infrastructure.Persistance.Context;

namespace Organizaiton.Application.CQRS.Organizations.Queries;

/// <summary>
/// Получить организацию по идентификатору пользователя
/// </summary>
public record GetOrganizationByUserIdQuery(Guid UserId) : IQuery<OrganizationDto>;

/// <summary>
/// Хандлер получения организацию по идентификатору пользователя
/// </summary>
internal class GetOrganizationByUserIdQueryHandler : IQueryHandler<GetOrganizationByUserIdQuery, OrganizationDto>
{
    private readonly OrganizationDbContext _context;
    private readonly IMapper _mapper;

    public GetOrganizationByUserIdQueryHandler(OrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OrganizationDto> Handle(GetOrganizationByUserIdQuery request, CancellationToken cancellationToken)
    {
        var organization = await _context.Organizations
            .AsNoTracking()
            .Include(x => x.OrganizationUsers)
            .Include(x => x.Products)
            .Include(x => x.BuyOrders)
            .Include(x => x.SellOrders)
            .FirstOrDefaultAsync(x => x.OrganizationUsers.Select(user => user.UserId)
                .Contains(request.UserId), cancellationToken);

        if (organization is null)
            throw new NotFoundException("Организация не найдена");
        
        return _mapper.Map<OrganizationDto>(organization);
    }
}