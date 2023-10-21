using System.Linq.Expressions;
using Desafio_Balta_IBGE.Domain.Models;
using Desafio_Balta_IBGE.Domain.Interfaces.Queries;

namespace Desafio_Balta_IBGE.Domain.Queries;

public sealed class LocalityQueries : ILocalityQueries
{
    public Expression<Func<Ibge, bool>> GetByIbgeId(string ibgeId)
        => x => x.IbgeId == ibgeId; 

    public Expression<Func<Ibge, bool>> GetByCity(string city)
        => x => x.City.Contains(city);

    public Expression<Func<Ibge, bool>> GetByState(string state)
        => x => x.State == state;
}
