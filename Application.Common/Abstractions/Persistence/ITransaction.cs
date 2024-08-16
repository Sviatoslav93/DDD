using Result.Abstractions;

namespace Application.Common.Abstractions.Persistence;

public interface ITransaction
{
    public bool HasActiveTransaction { get; }

    Task<IResult> Execute(Func<Task<IResult>> execute, CancellationToken cancellationToken);
}
