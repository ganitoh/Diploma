using System.Reflection;
using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Organization.Domain.Models;
using Organization.Domain.Models.ManyToMany;

namespace Organization.Infrastructure.Persistance.Context;

public class OrganizationDbContext : BaseDbContext
{
    public OrganizationDbContext(DbContextOptions options) 
        : base(options) { }

    public DbSet<Domain.Models.Organization> Organization { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<ProductOrder> ProductOrders { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}