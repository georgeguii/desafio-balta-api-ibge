using FluentValidation;
using Desafio_Balta_IBGE.Application.UseCases.Locality.Request;

namespace Desafio_Balta_IBGE.Application.Validators.Locality;

public class DeleteLocalityValidator : AbstractValidator<DeleteLocalityRequest>
{
    public DeleteLocalityValidator()
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
    }
}
