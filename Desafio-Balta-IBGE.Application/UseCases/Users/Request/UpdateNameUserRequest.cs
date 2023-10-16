using Desafio_Balta_IBGE.Application.Validators.Users;
using Desafio_Balta_IBGE.Domain.Interfaces.Abstractions;
using FluentValidation.Results;

namespace Desafio_Balta_IBGE.Application.UseCases.Users.Request
{
    public class UpdateNameUserRequest : IRequest
    {
        public UpdateNameUserRequest(int userId, string name)
        {
            UserId = userId;
            Name = name;
        }
        public int UserId { get; set; }
        public string Name { get; set; }

        public ValidationResult Validar()
        {
            var validator = new UpdateUserValidator();
            return validator.Validate(this);
        }
    }
}
