using System.Reflection;
using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Notifications.Domain.Models;

namespace Notifications.Infrastructure.Persistance.Context;

public class NotificationDbContext : BaseDbContext
{
    public DbSet<Notification> Notifications { get; set; }
    
    public NotificationDbContext(DbContextOptions options) 
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}