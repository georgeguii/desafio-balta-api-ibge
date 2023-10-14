using Desafio_Balta_IBGE.Application.UseCases.Users.Request;
using Desafio_Balta_IBGE.Application.UseCases.Users.Response;
using MediatR;
using System.Net;

namespace Desafio_Balta_IBGE.Application.UseCases.Users.Handler
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
    {
        public CreateUserHandler()
        {
            
        }
        public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            #region Validações

            var result = request.Validar();

            if (!result.IsValid)
            {
                return new CreateUserResponse(StatusCode: HttpStatusCode.BadRequest,
                                             Message: "Requisição inválida. Por favor, valide os dados informados.",
                                             Errors: result.Errors);
            }

            #endregion

            return new CreateUserResponse(StatusCode: HttpStatusCode.OK,
                                             Message: "Usuário criado com sucesso.",
                                             Errors: result.Errors);
        }
    }
}
