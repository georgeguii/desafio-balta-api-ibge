using Desafio_Balta_IBGE.Domain.DTO;
using Desafio_Balta_IBGE.Domain.Interfaces.Queries;
using Desafio_Balta_IBGE.Domain.Interfaces.Services;
using Desafio_Balta_IBGE.Infra.Data.Context;
using Desafio_Balta_IBGE.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Desafio_Balta_IBGE.Infra.Services
{
    public sealed class QueriesServices : IQueriesServices
    {
        private readonly IbgeContext _ibgeContext;
        private readonly ILocalityQueries _localityQueries;

        public QueriesServices(ILocalityQueries localityQueries, IbgeContext ibgeContext)
        {
            _localityQueries = localityQueries;
            _ibgeContext = ibgeContext;
        }

        public async Task<IEnumerable<IbgeDTO>> GetAll(int pagina = 1, int tamanhoPagina = 50)
        {
            try
            {
                return await _ibgeContext
                             .Ibge
                             .Skip((pagina - 1) * tamanhoPagina)
                             .Take(tamanhoPagina)
                             .Select(x => new IbgeDTO(x.IbgeId, x.City, x.State))
                             .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao buscar todas as localidades. Detalhe: {ex.Message}");
            }
        }

        public async Task<IbgeDTO> GetLocalityByIbgeId(string ibgeId)
            => await _ibgeContext
                        .Ibge
                        .AsQueryable()
                        .Where(_localityQueries.GetByIbgeId(ibgeId.Trim().ToUpper()))
                        .Select(x => new IbgeDTO(x.IbgeId, x.City, x.State))
                        .FirstOrDefaultAsync() ?? throw new NotFoundLocalityException($"Localidade não encontrada.");

        public async Task<IEnumerable<IbgeDTO>> GetLocalityByCity(string city)
            => await _ibgeContext
                        .Ibge
                        .AsQueryable()
                        .Where(_localityQueries.GetByCity(city.Trim().ToUpper()))
                        .Select(x => new IbgeDTO(x.IbgeId, x.City, x.State))
                        .ToListAsync();

        public async Task<IEnumerable<IbgeDTO>> GetLocalitiesByState(string state)
            => await _ibgeContext
                        .Ibge
                        .AsQueryable()
                        .Where(_localityQueries.GetByState(state.Trim().ToUpper()))
                        .Select(x => new IbgeDTO(x.IbgeId, x.City, x.State))
                        .ToListAsync();
    }

    
}