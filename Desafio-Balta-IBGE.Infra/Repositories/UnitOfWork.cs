using Microsoft.EntityFrameworkCore.Storage;

using Desafio_Balta_IBGE.Infra.Data.Context;
using Desafio_Balta_IBGE.Domain.Interfaces.UnitOfWork;

namespace Desafio_Balta_IBGE.Infra.Repositories;

public sealed class UnitOfWork : IUnitOfWork
{
    private IDbContextTransaction? _transaction;
    private readonly IbgeContext _context;

    public UnitOfWork(IbgeContext ibgeContext)
    {
        _context = ibgeContext;
    }

    public void BeginTransaction()
    {
        _transaction = _context.Database.BeginTransaction();
    }

    public async Task Commit(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
        _transaction?.Commit();
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
