using Desafio_Balta_IBGE.Domain.Interfaces.IBGE;
using Desafio_Balta_IBGE.Domain.Models;
using Desafio_Balta_IBGE.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Desafio_Balta_IBGE.Infra.Repositories;

public sealed class IbgeRepository : IIbgeRepository
{
    private readonly IbgeContext _context;
    public IbgeRepository(IbgeContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Ibge entity)
    => await _context.Ibge.AddAsync(entity);

    public async Task<bool> IsIbgeCodeRegisteredAsync(string ibgeId)
        => await _context.Ibge.AnyAsync(x => x.IbgeId.Equals(ibgeId));

    public async Task<Ibge> GetByIdAsync(string id)
        => await _context.Ibge.Where(x => x.IbgeId.Equals(id)).FirstOrDefaultAsync();

    public async Task<bool> RemoveAsync(string id)
    {
        var result = await _context.Ibge.Where(x => x.IbgeId.Equals(id)).ExecuteDeleteAsync();
        return result != 0;
    }
}
