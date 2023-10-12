namespace Desafio_Balta_IBGE.Domain.Interfaces.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    void BeginTransaction();
    void Commit();
    void Rollback();
}
