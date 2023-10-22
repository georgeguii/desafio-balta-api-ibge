using FluentValidation;
using Desafio_Balta_IBGE.Application.UseCases.Locality.Request;

namespace Desafio_Balta_IBGE.Application.Validators.Locality;

public class UpdateStateLocalityValidator : AbstractValidator<UpdateStateLocalityRequest>
{
    public UpdateStateLocalityValidator()
    {
        RuleFor(x => x.IbgeId)
            .NotNull()
                .WithMessage("O código do IBGE não pode ser nulo.")
            .NotEmpty()
                .WithMessage("O código do IBGE não pode ser vazio.")
            .Length(7, 7)
                .WithMessage("O Código do IBGE deve possuir 7 caracteres.")
            .Matches(@"^[0-9]+$")
                .WithMessage("O código do IBGE deve possuir somente números.");

        RuleFor(l => l.State)
            .NotNull()
                .WithMessage("O estado não pode ser nulo.")
            .NotEmpty()
                .WithMessage("O estado não pode ser vazio.")
            .Length(2, 2)
                .WithMessage("O estado deve possuir 2 caracteres.")
            .Matches(@"^[A-Za-z]+$")
                .WithMessage("O estado deve possuir somente letras.");
    }
}