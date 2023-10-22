using Desafio_Balta_IBGE.Domain.Models;
using System.Linq.Expressions;

namespace Desafio_Balta_IBGE.Domain.Interfaces.Queries
{
    public interface IUserQueries
    {
        Expression<Func<User, bool>> GetByIdAsync(int id);
        Expression<Func<User, bool>> GetByEmailAsync(string email);
        Expression<Func<User, bool>> GetByNameAsync(string name);
    }
}
