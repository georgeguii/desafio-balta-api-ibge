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
    public class DeleteUserHandler : IDeleteUserHandler
    {
        private readonly IUserRepository __userRepository;
        private readonly IUnitOfWork __unitOfWork;
        public DeleteUserHandler()
        {
            
        }
        public DeleteUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            __userRepository = userRepository;
            __unitOfWork = unitOfWork;
        }

        public async Task<IResponse> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
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
                    return new NotFoundUser(StatusCode: HttpStatusCode.NotFound,
                                           Message: "Usuário informado não está cadastrado.");

                #endregion

                return await DeleteUser(userDB, cancellationToken);
                
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

        private async Task<IResponse> DeleteUser(User userDB, CancellationToken cancellationToken)
        {
            __unitOfWork.BeginTransaction();

            var deleted = await __userRepository.RemoveAsync(userDB);
            if (deleted == false)
                return new DeleteUserError(StatusCode: HttpStatusCode.InternalServerError,
                                         Message: "Houve uma falha na exclusão do usuário. Por favor, tente novamente mais tarde.");

            await __unitOfWork.Commit(cancellationToken);

            return new DeletedSuccessfully(StatusCode: HttpStatusCode.OK,
                                         Message: $"{userDB.Name} excluído com sucesso.");
        }
    }
}
