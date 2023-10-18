using FluentValidation.Results;
using Desafio_Balta_IBGE.Application.Validators.Locality;

namespace Desafio_Balta_IBGE.Application.UseCases.Locality.Request;

public class UpdateStateLocalityRequest
{
    public string State { get; init; }

    public UpdateStateLocalityRequest(string state)
    {
        State = state.Trim();
    }

    public ValidationResult Validar()
    {
        var validator = new UpdateStateLocalityValidator();
        return validator.Validate(this);
    }
}
