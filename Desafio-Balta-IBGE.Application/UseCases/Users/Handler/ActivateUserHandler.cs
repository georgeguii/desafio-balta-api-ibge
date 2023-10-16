using Desafio_Balta_IBGE.Application.Abstractions;
using Desafio_Balta_IBGE.Application.UseCases.Users.Request;
using Desafio_Balta_IBGE.Application.UseCases.Users.Response;
using Desafio_Balta_IBGE.Domain.Interfaces.Abstractions;
using Desafio_Balta_IBGE.Domain.Interfaces.Services;
using Desafio_Balta_IBGE.Domain.Interfaces.UnitOfWork;
using Desafio_Balta_IBGE.Domain.Interfaces.UserRepository;
using Desafio_Balta_IBGE.Domain.Models;
using System.Net;

namespace Desafio_Balta_IBGE.Application.UseCases.Users.Handler
{
    public class ActivateUserHandler : IActivateUserHandler
    {
        private readonly IUserRepository __userRepository;
        private readonly IUnitOfWork __unitOfWork;
        private readonly IEmailServices __emailServices;

        public ActivateUserHandler()
        {
            
        }

        public ActivateUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IEmailServices emailServices)
        {
            __userRepository = userRepository;
            __unitOfWork = unitOfWork;
            __emailServices = emailServices;
        }

        public async Task<ActivateUserResponse> Handle(ActivateUserRequest request, CancellationToken cancellationToken)
        {
            #region Validações

            var result = request.Validar();

            if (!result.IsValid)

                return new ActivateUserResponse(StatusCode: HttpStatusCode.BadRequest,
                                             Message: "Requisição inválida. Por favor, valide os dados informados.",
                                             Errors: result.Errors.ToDictionary(error => error.PropertyName, error => error.ErrorMessage));

            #endregion           

            try
            {
                __unitOfWork.BeginTransaction();

                #region Busca usuário pelo E-mail
                var userDB = await __userRepository.GetByEmailAsync(request.Email);
                if (userDB is null)
                    return new ActivateUserResponse(StatusCode: HttpStatusCode.BadRequest,
                                                    Message: "E-mail informado não está cadastrado.");

                #endregion

                #region Verifica código

                var codeResult = userDB.Email.VerifyEmail.VerifyCode(request.Code);
                if (!codeResult.IsCodeValid || codeResult is null)
                    return new ActivateUserResponse(StatusCode: HttpStatusCode.BadRequest,
                                                    Message: codeResult!.Message);

                #endregion

                #region Ativa a conta

                userDB.Email.VerifyEmail.ActivateAccount();

                var activated = await __userRepository.ActivateAccount(userDB);
                if (activated == false)
                {
                    __unitOfWork.Rollback();
                    return new ActivateUserResponse(StatusCode: HttpStatusCode.InternalServerError,
                                               Message: "Falha ao ativar conta do usuário. Por favor, tente novamente mais tarde.");
                }

                await __unitOfWork.Commit(cancellationToken);

                #endregion

                #region Envia E-mail de sucesso na ativação

                await __emailServices.SendActivationSuccess(userDB);
                return new ActivateUserResponse(StatusCode: HttpStatusCode.BadRequest,
                                               Message: $"{userDB.Name}, sua conta foi ativada com sucesso!");
                #endregion
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
