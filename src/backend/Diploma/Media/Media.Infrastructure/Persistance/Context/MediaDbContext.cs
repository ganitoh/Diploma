using System.Reflection;
using Media.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Media.Infrastructure.Persistance.Context;

public class MediaDbContext : DbContext
{
    public DbSet<MediaFile> MediaFiles { get; set; }

    public MediaDbContext(DbContextOptions options) 
        : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.ConfigureWarnings(warnings => warnings.Log(RelationalEventId .PendingModelChangesWarning));
    }
}