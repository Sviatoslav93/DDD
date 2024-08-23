using Application.Common.Abstractions.Services;

namespace ToDoList.Api.Services;

public class FakeCurrentUserService : ICurrentUserService
{
    public string UserId => "test";
}
