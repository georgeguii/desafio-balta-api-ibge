using Desafio_Balta_IBGE.Application.Abstractions;
using Desafio_Balta_IBGE.Application.UseCases.Users.Request;
using Desafio_Balta_IBGE.Application.UseCases.Users.Response;
using Desafio_Balta_IBGE.Domain.Interfaces.UnitOfWork;
using Desafio_Balta_IBGE.Domain.Interfaces.UserRepository;
using System.Net;

namespace Desafio_Balta_IBGE.Application.UseCases.Users.Handler
{
    public class LoginHandler : ILoginHandler
    {
        private readonly IUserRepository __userRepository;
        private readonly IUnitOfWork __unitOfWork;

        public LoginHandler()
        {
            
        }
        public LoginHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            __userRepository = userRepository;
            __unitOfWork = unitOfWork;
        }

        public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            #region Validações

            var result = request.Validar();

            if (!result.IsValid)

                return new LoginResponse(StatusCode: HttpStatusCode.BadRequest,
                                             Message: "Requisição inválida. Por favor, valide os dados informados.",
                                             Errors: result.Errors.ToDictionary(error => error.PropertyName, error => error.ErrorMessage));

            #endregion           

            try
            {
                #region Buscar usuário

                var userDB = await __userRepository.GetByEmailAsync(request.Email);
                if (userDB is null)
                    return new LoginResponse(StatusCode: HttpStatusCode.BadRequest,
                                             Message: "Usuário informado não está cadastrado.");

                #endregion

                #region Verifica se a conta está ativa

                if (!userDB.Email.VerifyEmail.Active)
                    return new LoginResponse(StatusCode: HttpStatusCode.BadRequest,
                                             Message: "Conta ainda não foi verificada. Por favor, verifique sua conta para ativa-la.");

                #endregion

                #region Valida senha

                if (!userDB.Password.Verify(request.Password, userDB.Password.Hash!))
                    return new LoginResponse(StatusCode: HttpStatusCode.BadRequest,
                                             Message: "Senha inválida.");

                #endregion

                #region Gera o token e response do usuário autenticado
                var response = new LoginResponse(StatusCode: HttpStatusCode.OK,
                                                Message: $"{userDB.Name} autenticado com sucesso!",
                                                token: "");
                #endregion

                return response;
            }
            catch (Exception)
            {
                __unitOfWork.Rollback();
                throw;
            }
            finally
            {
                __unitOfWork.Dispose();
            }
        }
    }
}
