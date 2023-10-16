using Microsoft.EntityFrameworkCore;
using Desafio_Balta_IBGE.Domain.Models;
using Desafio_Balta_IBGE.Infra.Data.Context;
using Desafio_Balta_IBGE.Domain.Interfaces.IBGE;
using Azure.Core;

namespace Desafio_Balta_IBGE.Infra.Repositories;

public class IbgeRepository : BaseRepository<Ibge>, IIbgeRepository
{
    public IbgeRepository(IbgeContext context) : base(context) { }

    public async Task<bool> IsIbgeCodeRegisteredAsync(string ibgeId)
        => await _dbSet.AnyAsync(x => x.IbgeId.Equals(ibgeId));

    public async Task<Ibge> GetByCityAsync(string city)
        => await _dbSet.FirstOrDefaultAsync(x => x.City.Contains(city));


    public async Task<Ibge> GetByStateAsync(string state)
        => await _dbSet.FirstOrDefaultAsync(x => x.State.Contains(state));


}
