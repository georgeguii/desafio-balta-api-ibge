using Desafio_Balta_IBGE.Application.Abstractions.Users;
using Desafio_Balta_IBGE.Application.UseCases.Users.Request;
using Desafio_Balta_IBGE.Application.UseCases.Users.Response;
using Desafio_Balta_IBGE.Domain.Interfaces.Abstractions;
using Desafio_Balta_IBGE.Domain.Interfaces.UnitOfWork;
using Desafio_Balta_IBGE.Domain.Interfaces.UserRepository;
using Desafio_Balta_IBGE.Domain.Models;
using System.Net;

namespace Desafio_Balta_IBGE.Application.UseCases.Users.Handler
{
    public class LoginHandler : ILoginHandler
    {
        private readonly IUserRepository __userRepository;
        private readonly IUnitOfWork __unitOfWork;
        private readonly ITokenServices __tokenServices;

        public LoginHandler()
        {
            
        }
        public LoginHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, ITokenServices tokenServices)
        {
            __userRepository = userRepository;
            __unitOfWork = unitOfWork;
            __tokenServices = tokenServices;
        }

        public async Task<IResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            #region Validações

            var result = request.Validar();

            if (!result.IsValid)
                return new InvalidRequest(StatusCode: HttpStatusCode.BadRequest,
                                          Message: "Requisição inválida. Por favor, valide os dados informados.",
                                          Errors: result.Errors.ToDictionary(error => error.PropertyName, error => error.ErrorMessage));

            #endregion           

            try
            {
                #region Buscar usuário

                var userDB = await __userRepository.GetByEmailAsync(request.Email!.Trim());
                if (userDB is null)
                    return new NotFoundUser(StatusCode: HttpStatusCode.BadRequest,
                                            Message: "Usuário informado não está cadastrado.");

                #endregion

                #region Verifica se a conta está ativa

                if (!userDB.Email.VerifyEmail.Active)
                    return new NotVerifiedAccount(StatusCode: HttpStatusCode.BadRequest,
                                             Message: "Conta ainda não foi verificada. Por favor, verifique sua conta para ativa-la.");

                #endregion

                #region Valida senha

                var isEquals = userDB.Password.Verify(request.Password!.Trim());
                if (!isEquals)
                    return new InvalidPassword(StatusCode: HttpStatusCode.BadRequest,
                                               Message: "Senha inválida.");

                #endregion

                #region Gera o token e response do usuário autenticado
                return SuccessLogin(userDB);
                #endregion
            }
            catch (Exception ex)
            {
                __unitOfWork.Rollback();
                throw new Exception($"Falha ao efetuar login. Detalhe: {ex.Message}");
            }
            finally
            {
                __unitOfWork.Dispose();
            }
        }

        private LoginSuccessfully SuccessLogin(User userDB)
        {
            var token = __tokenServices.GerarToken(userDB);
            var response = new LoginSuccessfully(StatusCode: HttpStatusCode.OK,
                                            Message: $"{userDB.Name} autenticado com sucesso!",
                                            token: token);


            return response;
        }
    }
}
