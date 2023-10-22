using Desafio_Balta_IBGE.Domain.DTO;
using Desafio_Balta_IBGE.Shared.Results;

namespace Desafio_Balta_IBGE.Domain.Interfaces.Services
{
    public interface IUserQueriesServices
    {
        Task<Result<IEnumerable<UserDTO>>> GetAllAsync();
        Task<Result<UserDTO>> GetBydIdAsync(int id);
        Task<Result<UserDTO>> GetByEmailAsync(string email);
        Task<Result<IEnumerable<UserDTO>>> GetByNameAsync(string name);
    }
}
