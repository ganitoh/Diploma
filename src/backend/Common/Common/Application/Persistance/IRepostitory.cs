namespace Common.Application.Persistance;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync<TEntityId>(TEntityId id);
    Task<TEntity[]> GetAllAsync(CancellationToken cancellationToken);
    void Create(TEntity entity);
    void Remove<TEntityId>(TEntity entity);
}