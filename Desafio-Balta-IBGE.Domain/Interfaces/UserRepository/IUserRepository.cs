using Desafio_Balta_IBGE.Domain.Models;

namespace Desafio_Balta_IBGE.Domain.Interfaces.UserRepository
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task<bool> IsEmailRegisteredAsync(string email);
        Task<bool> ActivateAccount(User user);
        Task AddAsync(User user);
    }
}
