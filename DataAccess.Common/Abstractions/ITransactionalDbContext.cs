namespace DataAccess.Common.Abstractions;

public interface ITransactionalDbContext<T>
    where T : IAsyncDisposable
{
    bool HasActiveTransaction { get; }

    Task<T> BeginTransactionAsync(CancellationToken cancellationToken = default);

    Task CommitTransactionAsync(T transaction, CancellationToken cancellationToken);

    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
