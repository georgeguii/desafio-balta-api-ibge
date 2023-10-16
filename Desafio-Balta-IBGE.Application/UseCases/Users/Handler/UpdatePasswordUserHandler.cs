using Desafio_Balta_IBGE.Application.Abstractions;
using Desafio_Balta_IBGE.Application.UseCases.Users.Request;
using Desafio_Balta_IBGE.Application.UseCases.Users.Response;
using Desafio_Balta_IBGE.Domain.Interfaces.UnitOfWork;
using Desafio_Balta_IBGE.Domain.Interfaces.UserRepository;
using Desafio_Balta_IBGE.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_Balta_IBGE.Application.UseCases.Users.Handler
{

    public class UpdatePasswordUserHandler : IUpdatePasswordUserHandler
    {
        private readonly IUserRepository __userRepository;
        private readonly IUnitOfWork __unitOfWork;

        public UpdatePasswordUserHandler()
        {
            
        }
        public UpdatePasswordUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            __userRepository = userRepository;
            __unitOfWork = unitOfWork;
        }

        public async Task<UpdatePasswordUserResponse> Handle(UpdatePasswordUserRequest request, CancellationToken cancellationToken)
        {
            #region Validações

            var result = request.Validar();

            if (!result.IsValid)

                return new UpdatePasswordUserResponse(StatusCode: HttpStatusCode.BadRequest,
                                             Message: "Requisição inválida. Por favor, valide os dados informados.",
                                             Errors: result.Errors.ToDictionary(error => error.PropertyName, error => error.ErrorMessage));

            #endregion           

            try
            {
                #region Buscar usuário

                var userDB = await __userRepository.GetByEmailAsync(request.Email);
                if (userDB is null)
                    return new UpdatePasswordUserResponse(StatusCode: HttpStatusCode.BadRequest,
                                             Message: "Usuário informado não está cadastrado.");

                #endregion

                #region Atualizar usuário

                userDB.Password.UpdatePassword(request.Password);

                __unitOfWork.BeginTransaction();

                var updated = await __userRepository.UpdatePasswordAsync(userDB);
                if (updated == false)
                    return new UpdatePasswordUserResponse(StatusCode: HttpStatusCode.InternalServerError,
                                             Message: "Houve uma falha na atualização dos dados do usuário. Por favor, tente novamente mais tarde.");

                await __unitOfWork.Commit(cancellationToken);

                #endregion

                return new UpdatePasswordUserResponse(StatusCode: HttpStatusCode.OK,
                                             Message: $"Email do usuário {userDB.Name} atualizado com sucesso!");
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
