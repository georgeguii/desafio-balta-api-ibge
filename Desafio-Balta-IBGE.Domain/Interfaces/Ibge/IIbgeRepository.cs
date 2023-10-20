using Desafio_Balta_IBGE.Domain.Models;

namespace Desafio_Balta_IBGE.Domain.Interfaces.IBGE;

public interface IIbgeRepository
{
    Task AddAsync(Ibge entity);
    Task<bool> IsIbgeCodeRegisteredAsync(string ibgeId);
    Task<Ibge> GetByIdAsync(string id);
    Task<bool> UpdateCityAsync(Ibge ibge);
    Task<bool> UpdateStateAsync(Ibge ibge);
    Task<bool> RemoveAsync(string id);
}
