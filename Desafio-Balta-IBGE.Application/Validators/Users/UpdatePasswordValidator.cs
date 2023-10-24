using Desafio_Balta_IBGE.Application.UseCases.Users.Request;
using FluentValidation;

namespace Desafio_Balta_IBGE.Application.Validators.Users
{
    public class UpdatePasswordValidator : AbstractValidator<UpdatePasswordUserRequest>
    {
        public UpdatePasswordValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty()
                    .WithMessage("Senha inválido.")
                .Matches(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")
                    .WithMessage("A senha deve conter ao menos 8 dígitos, 1 letra, 1 caractere especial, 1 letra minuscula e 1 letra maiuscula.");
        }
    }
}
