using Desafio_Balta_IBGE.Application.Abstractions;
using Desafio_Balta_IBGE.Application.UseCases.Users.Request;
using Desafio_Balta_IBGE.Application.UseCases.Users.Response;
using Desafio_Balta_IBGE.Domain.Interfaces.UnitOfWork;
using Desafio_Balta_IBGE.Domain.Interfaces.UserRepository;
using Desafio_Balta_IBGE.Domain.Models;
using Desafio_Balta_IBGE.Domain.ValueObjects;
using System.Net;

namespace Desafio_Balta_IBGE.Application.UseCases.Users.Handler
{
    public class CreateUserHandler : ICreateUserHandler
    {
        private readonly IUserRepository __userRepository;
        private readonly IUnitOfWork __unitOfWork;

        public CreateUserHandler()
        {
            
        }
        public CreateUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            __userRepository = userRepository;
            __unitOfWork = unitOfWork;
        }
        public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            #region Validações

            var result = request.Validar();

            if (!result.IsValid)
                
                return new CreateUserResponse(StatusCode: HttpStatusCode.BadRequest,
                                             Message: "Requisição inválida. Por favor, valide os dados informados.",
                                             Errors: result.Errors.ToDictionary(error => error.PropertyName, error => error.ErrorMessage));

            #endregion           

            try
            {
                #region Verificar se o E-mail está cadastrado

                var emailCadastrado = await __userRepository.IsEmailRegisteredAsync(email: request.Email);
                if (emailCadastrado)
                    return new CreateUserResponse(StatusCode: HttpStatusCode.Conflict,
                                                 Message: "Este e-mail já está cadastrado.",
                                                 Errors: result.Errors.ToDictionary(error => error.PropertyName, error => error.ErrorMessage));

                #endregion

                return await AddUser(request, cancellationToken);

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

        private async Task<CreateUserResponse> AddUser(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var user = new User(name: request.Name,
                                                email: new Email(request.Email),
                                                password: new Password(request.Password),
                                                role: "Administrador",
                                                ibge: new Ibge());

            user.Email.VerifyEmail.GenerateCode();

            __unitOfWork.BeginTransaction();

            await __userRepository.AddAsync(user);

            await __unitOfWork.Commit(cancellationToken);

            return new CreateUserResponse(StatusCode: HttpStatusCode.Created,
                                          Message: "Usuário criado com sucesso. Por favor, verifique seu e-mail para ativar sua conta.",
                                          ActivationCode: user.Email.VerifyEmail.Code!,
                                          ExpireDate: user.Email.VerifyEmail.ExpireDate.ToString()!);
        }

    }
}
