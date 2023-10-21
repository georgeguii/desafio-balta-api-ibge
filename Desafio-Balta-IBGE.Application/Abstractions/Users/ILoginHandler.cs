using Desafio_Balta_IBGE.Application.UseCases.Users.Request;
using Desafio_Balta_IBGE.Domain.Interfaces.Abstractions;

namespace Desafio_Balta_IBGE.Application.Abstractions.Users
{
    public interface ILoginHandler
    {
        Task<IResponse> Handle(LoginRequest request, CancellationToken cancellationToken);
    }
}
