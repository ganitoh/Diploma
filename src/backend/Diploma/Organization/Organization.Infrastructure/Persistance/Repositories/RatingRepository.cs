using Common.Infrastructure;
using Organizaiton.Application.Common.Persistance;
using Organization.Domain.Models;
using Organization.Infrastructure.Persistance.Context;

namespace Organization.Infrastructure.Persistance.Repositories;

public class RatingRepository : Repository<Rating, OrganizationDbContext>, IRatingRepository
{
    public RatingRepository(OrganizationDbContext dbContext) 
        : base(dbContext) { }
}