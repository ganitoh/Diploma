namespace Common.Application.Persistance;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetById<TEntityId>(TEntityId id, CancellationToken cancellationToken);
    Task<TEntity[]> GetAll(CancellationToken cancellationToken);
    void Create(TEntity entity);
    void Remove<TEntityId>(TEntity entity);
}