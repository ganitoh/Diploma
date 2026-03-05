using System.Reflection;
using Analytics.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Analytics.Infrastructure.Persistance.Context;

public class AnalyticsDbContext : DbContext
{
    public DbSet<OrderAnalytics> OrderAnalytics { get; set; }
    public DbSet<OrderItemAnalytics> OrderItemAnalytics { get; set; }
    public DbSet<OrganizationAnalytics> OrganizationAnalytics { get; set; }

    public AnalyticsDbContext(DbContextOptions options) 
        : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.ConfigureWarnings(warnings => warnings.Log(RelationalEventId.PendingModelChangesWarning));
    }

}