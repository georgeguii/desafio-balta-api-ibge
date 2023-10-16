using Desafio_Balta_IBGE.Application.Validators.Users;
using Desafio_Balta_IBGE.Domain.Interfaces.Abstractions;
using FluentValidation.Results;

namespace Desafio_Balta_IBGE.Application.UseCases.Users.Request
{
    public class LoginRequest : IRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public ValidationResult Validar()
        {
            var validator = new LoginValidator();
            return validator.Validate(this);
        }
    }
}
