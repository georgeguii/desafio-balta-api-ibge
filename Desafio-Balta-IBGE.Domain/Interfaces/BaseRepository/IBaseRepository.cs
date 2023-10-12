using System.Linq.Expressions;

namespace Desafio_Balta_IBGE.Domain.Interfaces.BaseRepository;

public interface IBaseRepository<TEntity>
{
    Task<IEnumerable<TEntity>> GetAsync();
    ValueTask<TEntity?> GetByIdAsync(int id);
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    void DeleteAsync(TEntity entity);

}
