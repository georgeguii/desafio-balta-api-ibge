using Desafio_Balta_IBGE.Application.UseCases.Users.Request;
using Desafio_Balta_IBGE.Application.UseCases.Users.Response;

namespace Desafio_Balta_IBGE.Application.Abstractions
{
    public interface IUpdateNameUserHandler
    {
        Task<UpdateNameUserResponse> Handle(UpdateNameUserRequest request, CancellationToken cancellationToken);
    }
}
