using Application.Common.Abstractions.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ToDoList.Application.Behaviours;

public class LoggingBehaviour<TRequest, TResponse>(
    ILogger<TRequest> logger,
    ICurrentUserService currentUserService) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
{
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = currentUserService.UserId;

        logger.LogInformation(
            "ToDoList Request: {Name} {@UserId} {@Request}",
            requestName,
            userId,
            request);

        return next();
    }
}
