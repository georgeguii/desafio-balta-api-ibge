using FluentValidation;
using Desafio_Balta_IBGE.Application.UseCases.Locality.Request;

namespace Desafio_Balta_IBGE.Application.Validators.Locality;

public class GetByStateLocalityValidator : AbstractValidator<GetByStateLocalityRequest>
{
    public GetByStateLocalityValidator()
    {
        RuleFor(l => l.State)
            .NotEmpty()
                .WithMessage("O estado não pode ser vazio.")
            .Length(2, 2)
                .WithMessage("O estado deve possuir 2 caracteres.")
            .Matches(@"^[A-Za-z]+$")
                .WithMessage("O estado deve possuir somente números.");
    }
}
