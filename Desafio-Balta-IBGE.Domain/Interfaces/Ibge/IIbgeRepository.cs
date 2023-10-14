using Desafio_Balta_IBGE.Domain.Models;
using Desafio_Balta_IBGE.Domain.Interfaces.BaseRepository;

namespace Desafio_Balta_IBGE.Domain.Interfaces.IBGE;

public interface IIbgeRepository : IBaseRepository<Ibge>
{
    Task<Ibge> GetByCityAsync(string city);
    Task<Ibge> GetByStateAsync(string state);
}
