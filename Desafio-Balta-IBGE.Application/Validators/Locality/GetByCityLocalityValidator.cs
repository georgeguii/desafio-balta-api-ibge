using FluentValidation;
using Desafio_Balta_IBGE.Application.UseCases.Locality.Request;

namespace Desafio_Balta_IBGE.Application.Validators.Locality;
public class GetByCityLocalityValidator : AbstractValidator<GetByCityLocalityRequest>
{
    public GetByCityLocalityValidator()
    {
        RuleFor(l => l.City)
            .NotEmpty()
                .WithMessage("A cidade não pode ser vazia.")
            .MaximumLength(80)
                .WithMessage("A cidade deve possuir no máximo 80 caracteres.")
            .Matches(@"^[A-Za-zÀ-ú\s'-]+$")
                .WithMessage("A cidade deve possuir somente caracteres válidos");
    }
}
