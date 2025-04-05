using Common.Infrastructure;
using Organization.Application.Commnon.Persistance.Repositories;
using Organization.Infrastructure.Persistance.Context;

namespace Organization.Infrastructure.Persistance.Repositories;

public class OrganizationRepository : Repository<Domain.Models.Organization, OrganizationDbContext>, IOrganizationRepository
{
    public OrganizationRepository(OrganizationDbContext dbContext) 
        : base(dbContext) { }
}