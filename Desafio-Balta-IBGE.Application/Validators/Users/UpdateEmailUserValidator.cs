using Desafio_Balta_IBGE.Application.UseCases.Users.Request;
using FluentValidation;

namespace Desafio_Balta_IBGE.Application.Validators.Users
{
    public class UpdateEmailUserValidator : AbstractValidator<UpdateEmailUserRequest>
    {
        public UpdateEmailUserValidator()
        {
            RuleFor(x => x.Email)
               .NotEmpty()
                   .WithMessage("Email inválido.")
               .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
                   .WithMessage("Por favor, insira um endereço de e-mail válido.");
        }
    }
}
