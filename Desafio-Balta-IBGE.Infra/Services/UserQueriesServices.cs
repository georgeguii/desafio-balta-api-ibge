using Desafio_Balta_IBGE.Domain.DTO;
using Desafio_Balta_IBGE.Domain.Interfaces.Queries;
using Desafio_Balta_IBGE.Domain.Interfaces.Services;
using Desafio_Balta_IBGE.Infra.Data.Context;
using Desafio_Balta_IBGE.Shared.Results;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Desafio_Balta_IBGE.Infra.Services
{
    public sealed class UserQueriesServices : IUserQueriesServices
    {
        private readonly IbgeContext _ibgeContext;
        private readonly IUserQueries _userQueries;

        public UserQueriesServices(IbgeContext ibgeContext, IUserQueries userQueries)
        {
            _ibgeContext = ibgeContext;
            _userQueries = userQueries;
        }

        public async Task<Result<IEnumerable<UserDTO>>> GetAllAsync()
        {
            try
            {
                var users = await _ibgeContext
                    .User
                    .AsNoTracking()
                    .Select(x => new UserDTO
                    {
                        UserId = x.UserId,
                        Name = x.Name,
                        Email = x.Email,
                        Role = x.Role
                    }).ToListAsync();

                return Result<IEnumerable<UserDTO>>.Success(users);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Result<UserDTO>> GetBydIdAsync(int id)
        {
            try
            {
                var user = await _ibgeContext
                            .User
                            .AsNoTracking()
                            .Where(_userQueries.GetByIdAsync(id))
                            .Select(x => new UserDTO
                            {
                                UserId = x.UserId,
                                Name = x.Name,
                                Email = x.Email,
                                Role = x.Role
                            }).FirstOrDefaultAsync();
                if (user is null)
                    return Result<UserDTO>.NotFound(HttpStatusCode.NotFound, $"Nenhum usuário com Id {id} encontrado");

                return Result<UserDTO>.Success(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Result<UserDTO>> GetByEmailAsync(string email)
        {
            try
            {
                var user = await _ibgeContext
                            .User
                            .AsNoTracking()
                            .Where(_userQueries.GetByEmailAsync(email))
                            .Select(x => new UserDTO
                            {
                                UserId = x.UserId,
                                Name = x.Name,
                                Email = x.Email,
                                Role = x.Role
                            }).FirstOrDefaultAsync();
                if (user is null)
                    return Result<UserDTO>.NotFound(HttpStatusCode.NotFound, $"Nenhum usuário com E-mail {email} encontrado");

                return Result<UserDTO>.Success(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<UserDTO>>> GetByNameAsync(string name)
        {
            try
            {
                var users = await _ibgeContext
                            .User
                            .AsNoTracking()
                            .Where(_userQueries.GetByNameAsync(name))
                            .Select(x => new UserDTO
                            {
                                UserId = x.UserId,
                                Name = x.Name,
                                Email = x.Email,
                                Role = x.Role
                            }).ToListAsync();

                if (users is null)
                    return Result<IEnumerable<UserDTO>>.NotFound(HttpStatusCode.NotFound, $"Nenhum usuário com nome {name} encontrado");

                return Result<IEnumerable<UserDTO>>.Success(users);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
