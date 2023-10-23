using Desafio_Balta_IBGE.Application.Validators.Users;
using FluentValidation.Results;

namespace Desafio_Balta_IBGE.Application.UseCases.Users.Request
{
    public class UpdatePasswordUserRequest
    {
        public string Password { get; set; }

        public ValidationResult Validar()
        {
            var validator = new UpdatePasswordValidator();
            return validator.Validate(this);
        }
    }
}
