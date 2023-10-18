using Desafio_Balta_IBGE.Domain.Models;
using Desafio_Balta_IBGE.Domain.Interfaces.BaseRepository;

namespace Desafio_Balta_IBGE.Domain.Interfaces.IBGE;

public interface IIbgeRepository : IBaseRepository<Ibge>
{
    Task<bool> IsIbgeCodeRegisteredAsync(string ibgeId);
    Task<Ibge> GetByIdAsync(string id);
    Task<Ibge> GetByCityAsync(string city);
    Task<IEnumerable<Ibge>> GetByStateAsync(string state);
}
