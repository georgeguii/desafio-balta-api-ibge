using Desafio_Balta_IBGE.Application.UseCases.Users.Request;
using FluentValidation;

namespace Desafio_Balta_IBGE.Application.Validators.Users
{
    public class ActivateUserValidator : AbstractValidator<ActivateUserRequest>
    {
        public ActivateUserValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                    .WithMessage("Email inválido.")
                .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
                    .WithMessage("Por favor, insira um endereço de e-mail válido.");

            RuleFor(x => x.Code)
                .NotEmpty()
                    .WithMessage("Código inválido.")
                .Length(6, 6)
                    .WithMessage("Código inválido. Deve conter 6 dígitos.");
        }
    }
}
