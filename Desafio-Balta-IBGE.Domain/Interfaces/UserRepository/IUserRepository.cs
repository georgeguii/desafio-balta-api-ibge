﻿using Desafio_Balta_IBGE.Domain.Models;

namespace Desafio_Balta_IBGE.Domain.Interfaces.UserRepository
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByIdAsync(int id);
        Task<bool> IsEmailRegisteredAsync(string email);
        Task<bool> ActivateAccount(User user);
        Task AddAsync(User user);
        Task<bool> UpdateNameAsync(int id, User user);
        Task<bool> UpdateEmailAsync(User user);
        Task<bool> UpdatePasswordAsync(User user);
        Task<bool> RemoveAsync(User user);
    }
}
