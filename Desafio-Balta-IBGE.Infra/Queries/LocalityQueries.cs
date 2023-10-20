using Desafio_Balta_IBGE.Domain.Interfaces.Queries;
using Desafio_Balta_IBGE.Domain.Models;
using Desafio_Balta_IBGE.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Desafio_Balta_IBGE.Infra.Queries;

public class LocalityQueries : ILocalityQueries
{
    private readonly IbgeContext _ibgeContext;

    public LocalityQueries(IbgeContext ibgeContext)
    {
        _ibgeContext = ibgeContext;
    }

    public async Task<Ibge?> GetLocalityByCity(string city)
    {
        return await _ibgeContext
            .Ibge
            .AsNoTracking()
            .Where(x => x.City.Contains(city))
            .FirstOrDefaultAsync();
    }

    public async Task<Ibge?> GetLocalityByIbgeId(string ibgeId)
    {
        return await _ibgeContext
            .Ibge
            .AsNoTracking()
            .Where(x => x.IbgeId.Equals(ibgeId))
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Ibge>> GetLocalityByState(string state)
    {
        return await _ibgeContext
            .Ibge
            .AsNoTracking()
            .Where(x => x.State.Equals(state))
            .ToListAsync();
    }
}
