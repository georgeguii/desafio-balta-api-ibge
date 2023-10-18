using FluentValidation;
using Desafio_Balta_IBGE.Application.UseCases.Locality.Request;

namespace Desafio_Balta_IBGE.Application.Validators.Locality;

public class GetByIbgeIdLocalityValidator : AbstractValidator<GetByIbgeIdLocalityRequest>
{
    public GetByIbgeIdLocalityValidator()
    {
        RuleFor(x => x.IbgeId)
            .NotEmpty()
                .WithMessage("O código do IBGE não pode ser vazio.")
            .Length(7, 7)
                .WithMessage("O Código do IBGE deve possuir 7 caracteres.")
            .Matches(@"^[0-9]+$")
                .WithMessage("O código do IBGE deve possuir somente números.");
    }
}