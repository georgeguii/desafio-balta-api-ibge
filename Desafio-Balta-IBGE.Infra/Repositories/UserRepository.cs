using Desafio_Balta_IBGE.Domain.Interfaces.BaseRepository;
using Desafio_Balta_IBGE.Domain.Interfaces.UserRepository;
using Desafio_Balta_IBGE.Domain.Models;
using Desafio_Balta_IBGE.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Desafio_Balta_IBGE.Infra.Repositories
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly IbgeContext __context;
        private readonly IBaseRepository<User> __baseRepository;

        public UserRepository(IBaseRepository<User> baseRepository, IbgeContext context)
        {
            __baseRepository = baseRepository;
            __context = context;
        }

        public Task AddAsync(User user) 
            => __baseRepository.AddAsync(user);

        public async Task<bool> ActivateAccount(User user)
        {
            var activated = await __context
                            .User
                            .Where(x => x.Email.Address == user.Email.Address)
                            .ExecuteUpdateAsync(x => 
                                            x.SetProperty(x => x.Email.VerifyEmail.ExpireDate, user.Email.VerifyEmail.ExpireDate)
                                            .SetProperty(x => x.Email.VerifyEmail.ActivateDate, user.Email.VerifyEmail.ActivateDate)
                                            .SetProperty(x => x.Email.VerifyEmail.Code, user.Email.VerifyEmail.Code));

           
            return activated != 0;
        }

        public async Task<bool> UpdateNameAsync(int id, User user)
        {
            var updated = await __context
                    .User
                    .Where(x => x.UserId == id)
                    .ExecuteUpdateAsync(x =>
                                    x.SetProperty(x => x.Name, user.Name));

            return updated != 0;
        }

        public async Task<bool> UpdateEmailAsync(User user)
        {
            var updated = __context
                .User
                .Where(x => x.UserId == user.UserId)
                .ExecuteUpdate(x => x.SetProperty(x => x.Email.Address, user.Email.Address));

            return updated != 0;
        }

        public async Task<bool> UpdatePasswordAsync(User user)
        {
            var updated = __context
                .User
                .Where(x => x.UserId == user.UserId)
                .ExecuteUpdate(x =>
                                x.SetProperty(x => x.Password.Hash, user.Password.Hash));

            return updated != 0;
        }

        public async Task<bool> RemoveAsync(User user)
        {
            var deleted = await __context
                                .User
                                .Where(x => x.UserId == user.UserId)
                                .ExecuteDeleteAsync();

            return deleted != 0;
        }

        public async Task<User> GetByIdAsync(int id)
        => await __context
                 .User
                 .FirstOrDefaultAsync(x => x.UserId == id);

        public async Task<User> GetByEmailAsync(string email)
           => await __context
                 .User
                 .FirstOrDefaultAsync(x => x.Email.Address == email);

        public async Task<bool> IsEmailRegisteredAsync(string email)
            => await __context
                    .User
                    .AnyAsync(x => x.Email.Address == email);

        
    }
}
