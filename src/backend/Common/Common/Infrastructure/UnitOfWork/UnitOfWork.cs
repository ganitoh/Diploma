using Common.Domain;
using Common.Infrastructure.UserProvider;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.UnitOfWork;

public class UnitOfWork<TDbContext> : IUnitOfWork where  TDbContext : DbContext
{
    private readonly TDbContext _dbContext;
    private readonly IUserProvider _userProvider;

    public UnitOfWork(TDbContext dbContext, IUserProvider userProvider)
    {
        _dbContext = dbContext;
        _userProvider = userProvider;
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditableEntities();
        
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditableEntities()
    {
        var entries = _dbContext.ChangeTracker.Entries<IAuditableEntity>();

        var userId = _userProvider.GetUserId();
        var userName = _userProvider.GetUserName();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(w => w.CreatedBy).CurrentValue = userId;
                entry.Property(w => w.CreatedByName).CurrentValue = userName;
                entry.Property(w => w.CreatedOn).CurrentValue = DateTime.UtcNow;
            }
            
            if (entry.State == EntityState.Modified)
            {
                entry.Property(w => w.LastModifiedBy).CurrentValue = userId;
                entry.Property(w => w.LastModifiedByName).CurrentValue = userName;
                entry.Property(w => w.LastModifiedOn).CurrentValue = DateTime.UtcNow;
            }
        }
    }
}