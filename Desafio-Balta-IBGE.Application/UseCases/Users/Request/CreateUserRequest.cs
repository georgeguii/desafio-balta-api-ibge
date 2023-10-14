using Desafio_Balta_IBGE.Application.UseCases.Users.Response;
using Desafio_Balta_IBGE.Domain.Validators.Users;
using FluentValidation.Results;
using MediatR;

namespace Desafio_Balta_IBGE.Application.UseCases.Users.Request
{
    public class CreateUserRequest : IRequest<CreateUserResponse>
    {
        public CreateUserRequest(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
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
