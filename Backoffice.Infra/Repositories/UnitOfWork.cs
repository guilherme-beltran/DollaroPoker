using Backoffice.Domain.Interfaces.UnitOfWork;
using Backoffice.Infra.Contexts.Backoffice;
using Microsoft.EntityFrameworkCore.Storage;

namespace Backoffice.Infra.Repositories;

internal sealed class UnitOfWork : IUnitOfWork
{
    private IDbContextTransaction? _transaction;
    private readonly BackofficeContext _context;

    public UnitOfWork(BackofficeContext context) => _context = context;

    public void BeginTransaction()
    {
        _transaction = _context.Database.BeginTransaction();
    }

    public async Task Commit(CancellationToken cancellationToken)
    {
        try
        {
            await _context.SaveChangesAsync(cancellationToken);
            _transaction?.Commit();
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }
    }

    public void Rollback()
    {
        _transaction?.Rollback();
    }

    public void Dispose()
    {
        _transaction?.Dispose();
    }
}
