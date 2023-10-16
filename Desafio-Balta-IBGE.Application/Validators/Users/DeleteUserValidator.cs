using Desafio_Balta_IBGE.Application.UseCases.Users.Request;
using FluentValidation;

namespace Desafio_Balta_IBGE.Application.Validators.Users
{
    public class DeleteUserValidator : AbstractValidator<DeleteUserRequest>
    {
        public DeleteUserValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                    .WithMessage("Id informádo é inválido.");
        }
    }
}
