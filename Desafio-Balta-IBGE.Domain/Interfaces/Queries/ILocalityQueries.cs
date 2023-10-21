using System.Linq.Expressions;
using Desafio_Balta_IBGE.Domain.Models;

namespace Desafio_Balta_IBGE.Domain.Interfaces.Queries;

public interface ILocalityQueries
{
    Expression<Func<Ibge, bool>> GetByIbgeId(string ibgeId);
    Expression<Func<Ibge, bool>> GetByCity(string city);
    Expression<Func<Ibge, bool>> GetByState(string state);
}
