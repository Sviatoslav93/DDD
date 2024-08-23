using Application.Common.Abstractions.Persistence;
using DataAccess.Common.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ToDoList.DataAccess.Extensions;
using ToDoList.DataAccess.Interseptors;

namespace ToDoList.DataAccess.Contexts.Base;

public class BaseDbContext<TDbContext> : DbContext, ITransactionalDbContext<IDbContextTransaction>, IUnitOfWork
    where TDbContext : DbContext
{
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor = null!;
    private readonly IMediator _mediator = null!;

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value
    private IDbContextTransaction? _currentTransaction;
#pragma warning restore CS0649 // Field is never assigned to, and will always have its default value

    // ef core migrations
    public BaseDbContext(DbContextOptions<TDbContext> options)
        : base(options)
    {
    }

    public BaseDbContext(DbContextOptions<TDbContext> options, AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor, IMediator mediator)
        : base(options)
    {
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        _mediator = mediator;
    }

    public bool HasActiveTransaction => _currentTransaction is not null;

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (HasActiveTransaction)
        {
            throw new InvalidOperationException("There is alredy run transaction, can not execute another one");
        }

        _currentTransaction = await Database.BeginTransactionAsync(cancellationToken);
        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken)
    {
        if (_currentTransaction is null)
        {
            throw new InvalidOperationException("There is any transaction to commit");
        }

        if (transaction != _currentTransaction)
        {
            throw new InvalidOperationException($"Transaction with {transaction.TransactionId} is not current");
        }

        try
        {
            await SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception)
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            _currentTransaction?.Dispose();
            _currentTransaction = null;
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            if (_currentTransaction is null)
            {
                return;
            }

            await _currentTransaction.RollbackAsync(cancellationToken);
        }
        finally
        {
            _currentTransaction?.Dispose();
            _currentTransaction = null;
        }
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await SaveChangesAsync(cancellationToken);
        await _mediator.DispatchDomainEvents(this);

        return true;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);

        base.OnConfiguring(optionsBuilder);
    }
}
