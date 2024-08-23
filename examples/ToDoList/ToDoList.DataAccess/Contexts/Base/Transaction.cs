using Application.Common.Abstractions.Persistence;
using Result.Abstractions;

namespace ToDoList.DataAccess.Contexts.Base;

public abstract class Transaction<TDbContext>(TDbContext context) : ITransaction
    where TDbContext : BaseDbContext<TDbContext>
{
    public bool HasActiveTransaction => context.HasActiveTransaction;

    public async Task<IResult> Execute(Func<Task<IResult>> execute, CancellationToken cancellationToken)
    {
        await using var transaction = await context.BeginTransactionAsync(cancellationToken);
        var result = await execute();

        if (!result.IsSuccess)
        {
            await context.RollbackTransactionAsync(cancellationToken);
            return result;
        }

        await context.CommitTransactionAsync(transaction, cancellationToken);
        return result;
    }
}
