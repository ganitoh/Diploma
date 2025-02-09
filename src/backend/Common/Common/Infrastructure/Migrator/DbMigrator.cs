using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.Migrator;

public class DbMigrator<TDbContext> where TDbContext : DbContext
{
    private readonly TDbContext _dbContext;

    public DbMigrator(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Migrate()
    {
        _dbContext.Database.Migrate();
    }
}