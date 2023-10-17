using Desafio_Balta_IBGE.Application.Abstractions;
using Desafio_Balta_IBGE.Application.UseCases.Users.Request;
using Desafio_Balta_IBGE.Application.UseCases.Users.Response;
using Desafio_Balta_IBGE.Domain.Interfaces.Abstractions;
using Desafio_Balta_IBGE.Domain.Interfaces.Services;
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
        private readonly IEmailServices __emailServices;

        public CreateUserHandler()
        {
            
        }
        public CreateUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IEmailServices emailServices)
        {
            __userRepository = userRepository;
            __unitOfWork = unitOfWork;
            __emailServices = emailServices;
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

                await AddUser(request, cancellationToken);

                return new CreateUserResponse(StatusCode: HttpStatusCode.Created,
                                            Message: "Usuário criado com sucesso. Por favor, verifique seu e-mail para ativar sua conta.",
                                            Errors: result.Errors.ToDictionary(error => error.PropertyName, error => error.ErrorMessage));

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

        private async Task AddUser(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var user = new User(name: request.Name,
                                                email: new Email(request.Email),
                                                password: new Password(request.Password));

            user.Email.VerifyEmail.GenerateCode();

            __unitOfWork.BeginTransaction();

            await __userRepository.AddAsync(user);

            //await SendEmail(user);

            await __unitOfWork.Commit(cancellationToken);
        }

        private async Task SendEmail(User user)
            => await __emailServices.SendVerificationMail(user);
    }
}
