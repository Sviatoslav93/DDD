using Application.Common.Abstractions.Services;

namespace ToDoList.Api.Services;

public class CurrentUserService(IHttpContextAccessor context) : ICurrentUserService
{
    public string UserId => context.HttpContext?.User.FindFirst("sub")?.Value ?? string.Empty;
}
