using Desafio_Balta_IBGE.Domain.Models;

namespace Desafio_Balta_IBGE.Domain.Interfaces.Queries;

public interface ILocalityQueries
{
    Task<Ibge> GetLocalityByIbgeId(string ibgeId);
    Task<Ibge> GetLocalityByCity(string city);
    Task<IEnumerable<Ibge>> GetLocalityByState(string state);
}
