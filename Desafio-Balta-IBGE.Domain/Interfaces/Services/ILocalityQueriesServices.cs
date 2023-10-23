using Desafio_Balta_IBGE.Domain.DTO;

namespace Desafio_Balta_IBGE.Domain.Interfaces.Services
{
    public interface ILocalityQueriesServices
    {
        Task<IEnumerable<IbgeDTO>> GetAll(int? page = null, int? pageSize = null);
        Task<IbgeDTO> GetLocalityByIbgeId(string ibgeId);
        Task<IEnumerable<IbgeDTO>> GetLocalityByCity(string city);
        Task<IEnumerable<IbgeDTO>> GetLocalitiesByState(string state);
    }
}
