using FluentValidation.Results;
using Desafio_Balta_IBGE.Application.Validators.Locality;

namespace Desafio_Balta_IBGE.Application.UseCases.Locality.Request;

public class GetByCityLocalityRequest
{
    public string City { get; init; }

    public GetByCityLocalityRequest(string city)
    {
        City = city.Trim();
    }

    public ValidationResult Validar()
    {
        var validator = new GetByCityLocalityValidator();
        return validator.Validate(this);
    }
}
