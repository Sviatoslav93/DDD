namespace Application.Common.Abstractions.Persistence;

public interface IUnitOfWork
{
    Task<bool> SaveEntities(CancellationToken cancellationToken = default);
}
