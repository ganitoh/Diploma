using Common.Infrastructure;
using Identity.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Persistance.Context;

public class IdentityDbContext : BaseDbContext
{
    public DbSet<User> Users { get; set; }
    
    public IdentityDbContext(DbContextOptions options) 
        : base(options) { }
}