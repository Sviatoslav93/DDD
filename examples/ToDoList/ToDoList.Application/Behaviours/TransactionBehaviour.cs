using Application.Common.Abstractions.Persistence;
using MediatR;
using Result.Abstractions;

namespace ToDoList.Application.Behaviours;

public class TransactionBehaviour<TRequest, TResponse>(ITransaction transaction) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IResult
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (transaction.HasActiveTransaction)
        {
            throw new InvalidOperationException("The transaction is already active transaction");
        }

        var response = await transaction.Execute(
            async () =>
            {
                var response = await next();

                return response as IResult
                    ?? throw new InvalidOperationException("The response must be IResult");
            },
            cancellationToken);

        return (TResponse)response;
    }
}
