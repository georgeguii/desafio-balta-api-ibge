using Microsoft.EntityFrameworkCore;
using Desafio_Balta_IBGE.Domain.Models;
using Desafio_Balta_IBGE.Infra.Data.Context;
using Desafio_Balta_IBGE.Domain.Interfaces.IBGE;

namespace Desafio_Balta_IBGE.Infra.Repositories;

public class IbgeRepository : BaseRepository<Ibge>, IIbgeRepository
{
    public IbgeRepository(IbgeContext context) : base(context) { }

    public async Task<bool> IsIbgeCodeRegisteredAsync(string ibgeId)
        => await _dbSet.AnyAsync(x => x.IbgeId.Equals(ibgeId));

    public async Task<Ibge> GetByIdAsync(string id)
        => await _dbSet.SingleOrDefaultAsync(x => x.IbgeId.Contains(id));

    public async Task<Ibge> GetByCityAsync(string city)
        => await _dbSet.SingleOrDefaultAsync(x => x.City.Contains(city));


}
