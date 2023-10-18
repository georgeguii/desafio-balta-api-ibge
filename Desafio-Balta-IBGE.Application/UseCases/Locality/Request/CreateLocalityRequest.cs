using FluentValidation.Results;

using Desafio_Balta_IBGE.Application.Validators.Locality;

namespace Desafio_Balta_IBGE.Application.UseCases.Locality.Request;

public class CreateLocalityRequest
{
    public string IbgeId { get; init; }
    public string City { get; init; }
    public string State { get; init; }


    public CreateLocalityRequest(string ibgeId, string city, string state)
    {
        IbgeId = ibgeId.Trim();
        City = city.Trim();
        State = state.Trim();
    }

    public ValidationResult Validar()
    {
        var validator = new CreateLocalityValidator();
        return validator.Validate(this);
    }
}
