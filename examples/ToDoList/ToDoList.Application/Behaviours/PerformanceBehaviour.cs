﻿using System.Diagnostics;
using Application.Common.Abstractions.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ToDoList.Application.Behaviours;

public class PerformanceBehaviour<TRequest, TResponse>(
    ILogger<TRequest> logger,
    ICurrentUserService currentUserService) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly Stopwatch _timer = new();

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds <= 500)
        {
            return response;
        }

        var requestName = typeof(TRequest).Name;
        var userId = currentUserService.UserId;

        /*var userName = string.IsNullOrEmpty(userId)
            ? string.Empty
            : await identityService.GetUserNameAsync(userId);*/
        var userName = string.Empty;

        logger.LogWarning(
            "Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
            requestName,
            elapsedMilliseconds,
            userId,
            userName,
            request);

        return response;
    }
}