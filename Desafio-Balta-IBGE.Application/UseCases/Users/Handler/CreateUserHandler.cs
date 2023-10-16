using Desafio_Balta_IBGE.Application.Abstractions;
using Desafio_Balta_IBGE.Application.UseCases.Users.Request;
using Desafio_Balta_IBGE.Application.UseCases.Users.Response;
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

                var emailCadastrado = await __userRepository.IsEmailRegistered(email: request.Email);
                if (emailCadastrado)
                    return new CreateUserResponse(StatusCode: HttpStatusCode.BadRequest,
                                                 Message: "Este e-mail já está cadastrado.",
                                                 Errors: result.Errors.ToDictionary(error => error.PropertyName, error => error.ErrorMessage));

                #endregion

                #region Adiciona o usuário

                var user = new User(name: request.Name,
                                email: new Email(request.Email),
                                password: new Password(request.Password));

                user.Inactive();
                user.Email.VerifyEmail.GenerateCode();

                __unitOfWork.BeginTransaction();

                await __userRepository.AddAsync(user);

                await __unitOfWork.Commit(cancellationToken);

                #endregion

                #region Envia E-mail com código de verificação

                await __emailServices.SendVerificationEmail(user);

                #endregion

                return new CreateUserResponse(StatusCode: HttpStatusCode.OK,
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
    }
}
