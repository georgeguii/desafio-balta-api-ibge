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

            if (activated == 0)
            {
                return false;
            }
            return true;
        }

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
