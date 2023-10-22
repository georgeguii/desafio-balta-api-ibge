using Desafio_Balta_IBGE.Domain.DTO;

namespace Desafio_Balta_IBGE.Domain.Interfaces.Services
{
    public interface IQueriesServices
    {
        Task<IEnumerable<IbgeDTO>> GetAll(int? pagina = null, int? tamanhoPagina = null);
        Task<IbgeDTO> GetLocalityByIbgeId(string ibgeId);
        Task<IEnumerable<IbgeDTO>> GetLocalityByCity(string city);
        Task<IEnumerable<IbgeDTO>> GetLocalitiesByState(string state);
    }
}
