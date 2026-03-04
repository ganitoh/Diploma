using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Organizaiton.Application.Common.Persistance;
using Organization.Infrastructure.Persistance.Context;

namespace Organization.Infrastructure.Persistance.Repositories;

public class OrganizationRepository : Repository<Domain.Models.Organization, OrganizationDbContext>, IOrganizationRepository
{
    public OrganizationRepository(OrganizationDbContext dbContext) 
        : base(dbContext) { }

    public async Task<Domain.Models.Organization?> GetOrganizationByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var organizationByUserId = await _dbContext.Organizations
            .Include(x => x.OrganizationUsers)
            .FirstOrDefaultAsync(x => x.OrganizationUsers.Select(user => user.UserId).Contains(userId), cancellationToken);
        
        return organizationByUserId;
    }
}