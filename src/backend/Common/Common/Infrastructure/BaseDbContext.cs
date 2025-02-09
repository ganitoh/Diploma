using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure;

public class BaseDbContext : DbContext
{
    public BaseDbContext(DbContextOptions options) 
        : base(options) {  }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}