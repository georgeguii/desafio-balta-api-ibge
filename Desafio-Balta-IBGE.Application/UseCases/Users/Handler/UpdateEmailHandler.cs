﻿using Desafio_Balta_IBGE.Application.Abstractions.Response;
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
    public class UpdateEmailHandler : IUpdateEmailHandler
    {
        private readonly IUserRepository __userRepository;
        private readonly IUnitOfWork __unitOfWork;

        public UpdateEmailHandler()
        {
            
        }
        public UpdateEmailHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            __userRepository = userRepository;
            __unitOfWork = unitOfWork;
        }

        public async Task<IResponse> Handle(UpdateEmailUserRequest request, CancellationToken cancellationToken)
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

                var userDB = await __userRepository.GetByIdAsync(request.UserId);
                if (userDB is null)
                    return new NotFoundUser(StatusCode: HttpStatusCode.BadRequest,
                                            Message: "Usuário informado não está cadastrado.");

                #endregion

                #region Atualizar usuário

                return await UpdateUser(request, userDB, cancellationToken);
                #endregion
            }
            catch (Exception ex)
            {
                __unitOfWork.Rollback();
                throw new Exception($"Falha ao atualizar usuário. Detalhe: {ex.Message}");
            }
            finally
            {
                __unitOfWork.Dispose();
            }
        }

        private async Task<IResponse> UpdateUser(UpdateEmailUserRequest request, User userDB, CancellationToken cancellationToken)
        {
            userDB.Email.UpdateEmail(request.Email.Trim());
            if (!userDB.Email.IsValid)
                return new DomainNotification(StatusCode: HttpStatusCode.BadRequest,
                                             Errors: userDB.Email.Errors);

            __unitOfWork.BeginTransaction();

            var updated = await __userRepository.UpdateEmailAsync(userDB);
            if (updated == false)
                return new UpdateUserError(StatusCode: HttpStatusCode.InternalServerError,
                                           Message: "Houve uma falha na atualização dos dados do usuário. Por favor, tente novamente mais tarde.");

            await __unitOfWork.Commit(cancellationToken);

            return new UpdatedSuccessfully(StatusCode: HttpStatusCode.OK,
                                         Message: $"Email do usuário {userDB.Name} atualizado com sucesso!");
        }
    }
}
