using Desafio_Balta_IBGE.Domain.Models;

namespace Desafio_Balta_IBGE.Domain.Interfaces.UserRepository
{
    public interface IUserRepository
    {
        Task<bool> IsEmailRegistered(string email);
        Task AddAsync(User user);
    }
}
