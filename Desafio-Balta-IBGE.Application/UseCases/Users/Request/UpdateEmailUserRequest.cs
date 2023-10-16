using Desafio_Balta_IBGE.Application.Validators.Users;
using Desafio_Balta_IBGE.Domain.Interfaces.Abstractions;
using FluentValidation.Results;

namespace Desafio_Balta_IBGE.Application.UseCases.Users.Request
{
    public class UpdateEmailUserRequest : IRequest
    {
        public int UserId { get; set; }
        public string Email { get; set; }

        public ValidationResult Validar()
        {
            var validator = new UpdateEmailUserValidator();
            return validator.Validate(this);
        }
    }
}
