namespace Common.Infrastructure.UnitOfWork;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken = default);
}