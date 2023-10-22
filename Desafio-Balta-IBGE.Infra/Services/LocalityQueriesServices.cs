using Desafio_Balta_IBGE.Domain.DTO;
using Desafio_Balta_IBGE.Domain.Interfaces.Queries;
using Desafio_Balta_IBGE.Domain.Interfaces.Services;
using Desafio_Balta_IBGE.Infra.Data.Context;
using Desafio_Balta_IBGE.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Desafio_Balta_IBGE.Infra.Services
{
    public sealed class LocalityQueriesServices : ILocalityQueriesServices
    {
        private readonly IbgeContext _ibgeContext;
        private readonly ILocalityQueries _localityQueries;

        public LocalityQueriesServices(ILocalityQueries localityQueries, IbgeContext ibgeContext)
        {
            _localityQueries = localityQueries;
            _ibgeContext = ibgeContext;
        }

        public async Task<IEnumerable<IbgeDTO>> GetAll(int? pagina = null, int? tamanhoPagina = null)
        {
            try
            {
                var query = _ibgeContext.Ibge.AsQueryable();

                if (pagina != null && tamanhoPagina != null)
                {
                    query = query.Skip((pagina.Value - 1) * tamanhoPagina.Value)
                                 .Take(tamanhoPagina.Value);
                }

                return await query.Select(x => new IbgeDTO(x.IbgeId, x.City, x.State))
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