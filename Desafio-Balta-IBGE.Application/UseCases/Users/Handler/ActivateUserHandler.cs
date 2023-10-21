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
    public class ActivateUserHandler : IActivateUserHandler
    {
        private readonly IUserRepository __userRepository;
        private readonly IUnitOfWork __unitOfWork;

        public ActivateUserHandler()
        {
            
        }

        public ActivateUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            __userRepository = userRepository;
            __unitOfWork = unitOfWork;
        }

        public async Task<IResponse> Handle(ActivateUserRequest request, CancellationToken cancellationToken)
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
                __unitOfWork.BeginTransaction();

                #region Busca usuário pelo E-mail
                var userDB = await __userRepository.GetByEmailAsync(request.Email!.Trim());
                if (userDB is null)
                    return new NotFoundUser(StatusCode: HttpStatusCode.BadRequest,
                                            Message: "E-mail informado não está cadastrado.");

                #endregion

                #region Verifica código

                var codeResult = userDB.Email.VerifyEmail.VerifyCode(request.Code!.Trim());
                if (!codeResult.IsCodeValid || codeResult is null)
                    return new InvalidCode(StatusCode: HttpStatusCode.BadRequest,
                                           Message: codeResult!.Message);

                #endregion

                bool activated = await ActivateAccount(userDB);
                if (activated == false)
                {
                    __unitOfWork.Rollback();
                    return new ActivateAccountError(StatusCode: HttpStatusCode.InternalServerError,
                                                    Message: "Falha ao ativar conta do usuário. Por favor, tente novamente mais tarde.");
                }

                await Save(userDB, cancellationToken);

                return new ActivatedSuccess(StatusCode: HttpStatusCode.BadRequest,
                                               Message: $"{userDB.Name}, sua conta foi ativada com sucesso!");
            }
            catch (Exception ex)
            {
                __unitOfWork.Rollback();
                throw new Exception($"Falha ao ativar conta do usuário. Detalhe: {ex.Message}");
            }
            finally
            {
                __unitOfWork.Dispose();
            }
        }

        private async Task Save(User userDB, CancellationToken cancellationToken)
        {
            await __unitOfWork.Commit(cancellationToken);
        }

        private async Task<bool> ActivateAccount(User userDB)
        {
            userDB.Email.VerifyEmail.ActivateAccount();

            var activated = await __userRepository.ActivateAccount(userDB);
            return activated;
        }
    }
}
