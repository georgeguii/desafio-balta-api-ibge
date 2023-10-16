using Desafio_Balta_IBGE.Application.UseCases.Users.Request;
using FluentValidation;

namespace Desafio_Balta_IBGE.Application.Validators.Users
{
    public class UpdateUserValidator : AbstractValidator<UpdateNameUserRequest>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
                   .WithMessage("Nome inválido.")
               .Length(2, 60)
                   .WithMessage("Nome deve ter entre 2 a 60 caracteres.");
        }
    }
}
