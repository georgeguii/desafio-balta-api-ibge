using Desafio_Balta_IBGE.Domain.DTO;

namespace Desafio_Balta_IBGE.Domain.Interfaces.Services
{
    public interface IQueriesServices
    {
        Task<IEnumerable<IbgeDTO>> GetAll(int pagina = 1, int tamanhoPagina = 10);
        Task<IbgeDTO> GetLocalityByIbgeId(string ibgeId);
        Task<IbgeDTO> GetLocalityByCity(string city);
        Task<IEnumerable<IbgeDTO>> GetLocalitiesByState(string state);
    }
}
