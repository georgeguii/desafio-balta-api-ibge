using FluentValidation.Results;
using Desafio_Balta_IBGE.Application.Validators.Locality;

namespace Desafio_Balta_IBGE.Application.UseCases.Locality.Request;

public class GetByStateLocalityRequest
{
    public string State { get; init; }

    public GetByStateLocalityRequest(string state)
    {
        State = state.Trim();
    }

    public ValidationResult Validar()
    {
        var validator = new GetByStateLocalityValidator();
        return validator.Validate(this);
    }
}
