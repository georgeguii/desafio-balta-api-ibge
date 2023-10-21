using Desafio_Balta_IBGE.Application.UseCases.Users.Request;
using Desafio_Balta_IBGE.Application.UseCases.Users.Response;
using Desafio_Balta_IBGE.Domain.Interfaces.Abstractions;

namespace Desafio_Balta_IBGE.Application.Abstractions.Users
{
    public interface IActivateUserHandler
    {
        Task<IResponse> Handle(ActivateUserRequest request, CancellationToken cancellationToken);
    }
}
