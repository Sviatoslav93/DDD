using MediatR;
using Result;

namespace Application.Common.Abstractions.Services;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result<Unit> Result, string UserId)> CreateUserAsync(string userName, string password);

    Task<Result<Unit>> DeleteUserAsync(string userId);
}
