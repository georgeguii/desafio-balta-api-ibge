using Desafio_Balta_IBGE.Application.UseCases.Users.Request;
using FluentValidation;

namespace Desafio_Balta_IBGE.Application.Validators.Users
{
    public class CreateUserValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                    .WithMessage("Nome inválido.")
                .Length(2, 60)
                    .WithMessage("Nome deve ter entre 2 a 60 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty()
                    .WithMessage("Email inválido.")
                .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
                    .WithMessage("Por favor, insira um endereço de e-mail válido.");

            RuleFor(x => x.Password)
                .NotEmpty()
                    .WithMessage("Senha inválido.")
                .Matches(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")
                    .WithMessage("A senha deve conter ao menos 8 dígitos, 1 letra, 1 caractere especial, 1 letra minuscula e 1 letra maiuscula.");

        }
    }
}
