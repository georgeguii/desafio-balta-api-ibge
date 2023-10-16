using Desafio_Balta_IBGE.Application.Validators.Users;
using Desafio_Balta_IBGE.Domain.Interfaces.Abstractions;
using FluentValidation.Results;

namespace Desafio_Balta_IBGE.Application.UseCases.Users.Request
{
    public class DeleteUserRequest : IRequest
    {
        public int UserId { get; set; }

        public DeleteUserRequest(int userId)
        {
            UserId = userId;
        }

        public ValidationResult Validar()
        {
            var validator = new DeleteUserValidator();
            return validator.Validate(this);
        }
    }
}
