namespace Common.Application.Persistance;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken);
}