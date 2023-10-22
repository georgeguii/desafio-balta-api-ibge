using Desafio_Balta_IBGE.Domain.Interfaces.Queries;
using Desafio_Balta_IBGE.Domain.Models;
using System.Linq.Expressions;

namespace Desafio_Balta_IBGE.Domain.Queries
{
    public sealed class UserQueries : IUserQueries
    {
        public Expression<Func<User, bool>> GetByIdAsync(int id)
        => x => x.UserId.Equals(id);

        public Expression<Func<User, bool>> GetByEmailAsync(string email)
            => x => x.Email.Address.Equals(email);

        public Expression<Func<User, bool>> GetByNameAsync(string name)
            => x => x.Name.Contains(name);
    }
}
