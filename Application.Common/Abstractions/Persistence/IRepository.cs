namespace Application.Common.Abstractions.Persistence;

public interface IRepository
{
    IUnitOfWork UnitOfWork { get; }
}
