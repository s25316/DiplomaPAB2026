using Diploma.Application.Interfaces.Database;
using Diploma.Database;
using Microsoft.EntityFrameworkCore.Storage;

namespace Diploma.Infrastructure.Services.Database;

public class UnitOfWork(DiplomaDbContext context) : IUnitOfWork
{
    private readonly IDbContextTransaction transaction = context.Database.BeginTransactionAsync().GetAwaiter().GetResult();
    private bool isDisposed = false;
    private bool isExecuted = false;


    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        if (isExecuted)
            return;

        await context.Database.CommitTransactionAsync(cancellationToken);
        isExecuted = true;
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if (isExecuted)
            return;

        await context.Database.RollbackTransactionAsync(cancellationToken);
        isExecuted = true;
    }


    protected virtual void Dispose(bool disposing)
    {
        if (isDisposed)
            return;

        if (disposing && !isExecuted)
        {
            transaction.Rollback();
        }

        transaction.Dispose();
        isDisposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        if (isDisposed)
            return;

        if (!isExecuted)
        {
            await transaction.RollbackAsync();
        }

        await transaction.DisposeAsync();
        isDisposed = true;
    }
}

public class UnitOfWorkFactory(DiplomaDbContext context) : IUnitOfWorkFactory
{
    public async Task<IUnitOfWork> CreateAsync(CancellationToken cancellationToken = default)
    {
        return new UnitOfWork(context);
    }
}