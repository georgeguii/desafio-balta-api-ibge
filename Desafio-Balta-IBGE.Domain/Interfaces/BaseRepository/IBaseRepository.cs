using System.Linq.Expressions;

namespace Desafio_Balta_IBGE.Domain.Interfaces.BaseRepository;

public interface IBaseRepository<TEntity>
{
    Task<IEnumerable<TEntity>> GetAsync();
    Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate);
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);

}
