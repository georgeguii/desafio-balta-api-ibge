using Desafio_Balta_IBGE.Application.Validators.Users;
using Desafio_Balta_IBGE.Domain.Interfaces.Abstractions;
using FluentValidation.Results;

namespace Desafio_Balta_IBGE.Application.UseCases.Users.Request
{
    public class CreateUserRequest : IRequest
    {
        public CreateUserRequest(string name, string email, string password)
        {
            Name = name.Trim();
            Email = email.Trim();
            Password = password.Trim();
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } 

        public ValidationResult Validar()
        {
            var validator = new CreateUserValidator();
            return validator.Validate(this);
        }
    }
}
