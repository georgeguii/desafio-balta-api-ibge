using Desafio_Balta_IBGE.Domain.Models;
using System.Linq.Expressions;

namespace Desafio_Balta_IBGE.Domain.Queries;

public static class LocalityQueries
{
    public static Expression<Func<Ibge, bool>> GetByIbgeId(string ibgeId)
        => x => x.IbgeId == ibgeId;

    public static Expression<Func<Ibge, bool>> GetByCity(string city)
        => x => x.City.Contains(city);

    public static Expression<Func<Ibge, bool>> GetByState(string state)
        => x => x.State == state;
}
