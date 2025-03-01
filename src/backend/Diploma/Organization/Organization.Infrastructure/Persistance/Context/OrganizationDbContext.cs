using System.Reflection;
using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Organization.Infrastructure.Persistance.Context;

public class OrganizationDbContext : BaseDbContext
{
    public OrganizationDbContext(DbContextOptions options) 
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}