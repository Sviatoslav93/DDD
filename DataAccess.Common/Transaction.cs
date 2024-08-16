using Application.Common.Abstractions.Persistence;
using DataAccess.Common.Abstractions;
using Result.Abstractions;

namespace DataAccess.Common;

public abstract class Transaction(ITransactionalDbContext context) : ITransaction
{
    public bool HasActiveTransaction => context.HasActiveTransaction;

    public async Task<IResult> Execute(Func<Task<IResult>> execute, CancellationToken cancellationToken)
    {
        await using var transaction = await context.BeginTransactionAsync(cancellationToken);
        var result = await execute();

        if (result.IsSuccess)
        {
            await context.CommitTransactionAsync(transaction, cancellationToken);
            return result;
        }

        await context.RollbackTransaction(cancellationToken);
        return result;
    }
}
