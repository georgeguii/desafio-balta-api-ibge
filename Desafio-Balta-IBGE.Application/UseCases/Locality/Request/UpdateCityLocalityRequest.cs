using FluentValidation.Results;
using Desafio_Balta_IBGE.Application.Validators.Locality;

namespace Desafio_Balta_IBGE.Application.UseCases.Locality.Request;

public class UpdateCityLocalityRequest
{
    public string IbgeId { get; init; }
    public string City { get; init; }

    public UpdateCityLocalityRequest(string ibgeId, string city)
    {
        IbgeId = ibgeId.Trim();
        City = city.Trim();
    }

    public ValidationResult Validar()
    {
        var validator = new UpdateCityLocalityValidator();
        return validator.Validate(this);
    }
}
