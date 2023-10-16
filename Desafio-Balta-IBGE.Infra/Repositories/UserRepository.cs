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

        public async Task<bool> IsEmailRegistered(string email)
            => await __context
                    .User
                    .AnyAsync(x => x.Email.Address == email);
    }
}
