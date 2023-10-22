using FluentValidation;
using Desafio_Balta_IBGE.Application.UseCases.Locality.Request;

namespace Desafio_Balta_IBGE.Application.Validators.Locality;

public class UpdateCityLocalityValidator : AbstractValidator<UpdateCityLocalityRequest>
{
    public UpdateCityLocalityValidator()
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

        RuleFor(l => l.City)
            .NotNull()
                .WithMessage("A cidade não pode ser nulo.")
            .NotEmpty()
                .WithMessage("A cidade não pode ser vazia.")
            .MaximumLength(80)
                .WithMessage("A cidade deve possuir no máximo 80 caracteres.")
            .Matches(@"^[A-Za-zÀ-ú\s'-]+$")
                .WithMessage("A cidade deve possuir somente caracteres válidos");
    }
}