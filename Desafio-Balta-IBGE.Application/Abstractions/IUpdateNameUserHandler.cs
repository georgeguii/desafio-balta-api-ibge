using Desafio_Balta_IBGE.Application.UseCases.Users.Request;
using Desafio_Balta_IBGE.Domain.Models;

namespace Desafio_Balta_IBGE.Application.Abstractions
{
    public interface IUpdateNameUserHandler
    {
        Task<UpdateNameUserResponse> Handle(UpdateNameUserRequest request, CancellationToken cancellationToken);
    }
}
