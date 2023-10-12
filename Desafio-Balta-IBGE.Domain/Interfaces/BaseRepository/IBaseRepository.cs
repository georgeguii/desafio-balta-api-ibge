using System.Linq.Expressions;

namespace Desafio_Balta_IBGE.Domain.Interfaces.BaseRepository;

public interface IBaseRepository<TEntity>
{
    Task<IEnumerable<TEntity>> Get();
    Task<TEntity> GetbyId(Expression<Func<TEntity, bool>> predicate);
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    void DeleteAsync(TEntity entity);

}
