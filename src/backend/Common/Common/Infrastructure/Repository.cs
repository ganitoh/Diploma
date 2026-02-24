using Common.Application.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Common.Infrastructure;

public class Repository<TEntity, TDbContext> 
    : IRepository<TEntity> where TEntity : class where TDbContext : DbContext 
{
    protected readonly TDbContext _dbContext;

    public Repository(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<TEntity> GetQuery(bool asTrackingEnabled = false) => 
        _dbContext.Set<TEntity>();

    public async Task<TEntity> GetById<TEntityId>(TEntityId id, CancellationToken cancellationToken) =>
        await _dbContext.Set<TEntity>().FindAsync(id, cancellationToken);

    public async Task<TEntity[]> GetAll(CancellationToken cancellationToken) =>
        await _dbContext.Set<TEntity>().ToArrayAsync(cancellationToken); 

    public void Create(TEntity entity)
    {
        if (entity is null)
            throw new NullReferenceException();

        _dbContext.Set<TEntity>().Add(entity);
    }

    public void Remove<TEntityId>(TEntity entity)
    {
        if (entity is null)
            throw new NullReferenceException();

        _dbContext.Set<TEntity>().Remove(entity);
    }
}