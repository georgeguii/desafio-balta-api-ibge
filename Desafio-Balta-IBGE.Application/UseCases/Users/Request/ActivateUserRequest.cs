using Desafio_Balta_IBGE.Application.Validators.Users;
using Desafio_Balta_IBGE.Domain.Interfaces.Abstractions;
using FluentValidation.Results;

namespace Desafio_Balta_IBGE.Application.UseCases.Users.Request
{
    public class ActivateUserRequest : IRequest
    {
        public ActivateUserRequest(string email, string code)
        {
            Email = email;
            Code = code;
        }

        public string Email { get; set; }
        public string Code { get; set; }

        public ValidationResult Validar()
        {
            var validator = new ActivateUserValidator();
            return validator.Validate(this);
        }
    }
}
