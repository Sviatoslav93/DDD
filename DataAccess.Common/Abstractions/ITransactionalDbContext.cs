namespace DataAccess.Common.Abstractions;

public interface ITransactionalDbContext
{
    bool HasActiveTransaction { get; }

    Task<IAsyncDisposable> BeginTransactionAsync(CancellationToken cancellationToken = default);

    Task CommitTransactionAsync(IAsyncDisposable transaction, CancellationToken cancellationToken);

    Task RollbackTransaction(CancellationToken cancellationToken = default);
}
