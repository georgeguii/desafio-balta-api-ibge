using FluentValidation.Results;

using Desafio_Balta_IBGE.Application.Validators.Locality;

namespace Desafio_Balta_IBGE.Application.UseCases.Locality.Request;

public class CreateLocalityRequest
{
    public CreateLocalityRequest(string ibgeId, string city, string state)
    {
        IbgeId = ibgeId;
        City = city;
        State = state;
    }

    public string IbgeId { get; set; }
    public string City { get; set; }
    public string State { get; set; }

    public ValidationResult Validar()
    {
        var validator = new CreateLocalityValidator();
        return validator.Validate(this);
    }
}
